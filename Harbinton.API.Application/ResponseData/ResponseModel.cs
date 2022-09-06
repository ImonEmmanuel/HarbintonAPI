using System.Net;

namespace Harbinton.API.Application.ResponseData
{
    public class ResponseModel
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public int ResponseCode { get; set; }
        public string TransactionRef { get; set; }
    }
}
