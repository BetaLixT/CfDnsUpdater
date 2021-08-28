using BetaLixT.CloudFlare;
using BetaLixT.CloudFlare.Models.Enums;
using BetaLixT.DnsUpdater.Api.Database.Context;
using BetaLixT.DnsUpdater.Api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BetaLixT.CloudFlare.Models.Entities;
using BetaLixT.DnsUpdater.Api.Exceptions;

namespace  BetaLixT.DnsUpdater.Api.Services
{
    public class DnsUpdaterService
    {
        private readonly CloudFlareClient _cloudFlareClient;
        private readonly DatabaseContext _databaseContext;
        public DnsUpdaterService(CloudFlareClient cloudFlareClient, DatabaseContext databaseContext)
        {
            this._cloudFlareClient = cloudFlareClient;
            this._databaseContext = databaseContext;
        }

        public async Task UpdateAllRecordsAsync()
        {
            var wavIp = await ServerIpHelper.GetMyWanIpAsync();
            var records = await this._databaseContext.DnsRecords.Where(x => x.Ip != wavIp).ToListAsync();
            foreach (var record in records)
            {
                if (record.CloudFlareId == null)
                {
                    var newRecord = await this.FetchOrAddAndFetchDnsRecordAsync(
                        record.ZoneId,
                        DnsRecordType.A,
                        record.RecordName,
                        wavIp,
                        1,
                        null,
                        true);

                    record.CloudFlareId = newRecord.Id;
                    record.Ip = newRecord.Content;
                }
                else
                {
                    var newRecord = await this._cloudFlareClient.UpdateDnsRecord(
                        record.CloudFlareId,
                        record.ZoneId,
                        DnsRecordType.A,
                        record.RecordName,
                        wavIp,
                        1,
                        null,
                        true);
                    record.Ip = wavIp;
                }
            }
            this._databaseContext.UpdateRange(records);
            await this._databaseContext.SaveChangesAsync();
        }

        public async Task<DnsRecord> FetchOrAddAndFetchDnsRecordAsync(
            string zoneIdentifier,
            DnsRecordType recordType,
            string dnsRecordName,
            string recordContent,
            int ttl,
            int? priority = null,
            bool? proxied = false)
        {
            var listResponse = await this._cloudFlareClient.ListDnsRecordsAsync(zoneIdentifier, Match.all, dnsRecordName);

            var record = listResponse?.Result.Where(x => x.Name == dnsRecordName).FirstOrDefault();

            if (record == null)
            {
                record = (await this._cloudFlareClient.CreateDnsRecord(
                    zoneIdentifier,
                    recordType,
                    dnsRecordName,
                    recordContent,
                    ttl,
                    priority,
                    proxied
                    )).Result;
            }

            if(record == null)
            {
                throw new EntityCheckFailedException((int)ErrorCodes.RecordingCreationFailed, ErrorCodes.RecordingCreationFailed.ToString());
            }

            return record;
        }

        public async Task<List<Database.Entities.DnsRecord>> ListRecordsAsync()
        {
            return await this._databaseContext.DnsRecords.ToListAsync();
        }

        public async Task<Database.Entities.DnsRecord> CreateRecordsAsync(string zoneId, string recordName)
        {
            var record = await this._databaseContext.DnsRecords.Where(x => x.RecordName == recordName).FirstOrDefaultAsync();
            if(record != null)
            {
                throw new EntityCheckFailedException((int)ErrorCodes.ConflictingRecord, ErrorCodes.ConflictingRecord.ToString());
            }

            var wavIp = await ServerIpHelper.GetMyWanIpAsync();
            var newRecord = await this.FetchOrAddAndFetchDnsRecordAsync(
                        zoneId,
                        DnsRecordType.A,
                        recordName,
                        wavIp,
                        1,
                        null,
                        true);

            record = new Database.Entities.DnsRecord
            {
                CloudFlareId = newRecord.Id,
                RecordName = recordName,
                ZoneId = zoneId,
                Ip = wavIp
            };

            var entityAdded = this._databaseContext.DnsRecords.Add(record);
            await this._databaseContext.SaveChangesAsync();
            return entityAdded.Entity;
        }
    }
}