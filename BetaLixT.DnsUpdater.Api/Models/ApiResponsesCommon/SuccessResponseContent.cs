using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetaLixT.DnsUpdater.Api.Models.ApiResponsesCommon
{
    public class SuccessResponseContent<T>
    {
        public SuccessResponseContent(T data)
        {
            ResultData = data;
        }
        public string StatusMessage { get; private set; } = ResponseContentStatusMessages.Success;
        public T ResultData { get; private set; }
    }

    public class SuccessResponseContent
    {
        public SuccessResponseContent()
        {
        }
        public string StatusMessage { get; private set; } = ResponseContentStatusMessages.Success;
    }
}
