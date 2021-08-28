using System;
namespace BetaLixT.DnsUpdater.Api.Exceptions
{
    public class EntityCheckFailedException : Exception
    {
        public readonly int Code;
        public EntityCheckFailedException(int code, string Message) : base(Message)
        {
            this.Code = code;
        }
    }
}
