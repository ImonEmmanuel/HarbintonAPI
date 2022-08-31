using Harbinton.API.Dto;
using Harbinton.API.Model;
using Harbinton.API.ResponseData;
using System.Net;

namespace Harbinton.API.Contract
{
    public interface ITransactionRepository
    {
        Task<ResponseModel> BillPayment(string accountNumber, BillDto bill);
        Task<TransactionDto> GetStatus(string TransactionRef);
        Task<ResponseModel> Transfer(string AccountNumber, string AccountNumberToTransfer, int AmountToTransfer);

        Task<ResponseModel> TransactionResponse(string Status, string TransactionType , string Message, Transaction transaction, HttpStatusCode code);
    }
}
