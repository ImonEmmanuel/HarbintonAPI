using System.Net;

namespace Harbinton.API.ResponseData
{
    public class ResponseModel
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public int ResponseCode { get; set; }
        public string TransactionRef { get; set; }
    }
}
