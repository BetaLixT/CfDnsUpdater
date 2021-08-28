using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetaLixT.DnsUpdater.Api
{
    public enum ErrorCodes
    {
        // - Records
        ConflictingRecord = 1000,
        RecordingCreationFailed,

        // - Common
        UnhandledError = 10000,
    }
}
