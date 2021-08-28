# Cloud Flare DNS Updater
**Read Me Is Under Construction**
## Build and Run
From repository root
```
docker build -d -t cfdnsupdater -f .\BetaLixT.DnsUpdater.Api\Dockerfile .
docker run -p 80:80  cfdnsupdater -e "CloudFlareOptions__Token=<CloudFlareToken>" -e "UpdateDnsOptions__Schedule=0 */10 * * * *"
```