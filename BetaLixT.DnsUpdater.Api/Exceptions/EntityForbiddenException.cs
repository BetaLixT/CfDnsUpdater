using System;

namespace BetaLixT.DnsUpdater.Api.Exceptions
{
    public class EntityForbiddenException : Exception
    {
        public readonly int Code;
        public EntityForbiddenException(int code, string Message) : base(Message)
        {
            this.Code = code;
        }
    }
}
