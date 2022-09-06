using Harbinton.API.Application.Dto.Transaction;
using Harbinton.API.Application.ResponseData;
using Harbinton.API.Domain.Model;
using System.Net;

namespace Harbinton.API.Application.Contracts.Persitence
{
    public interface ITransactionRepository
    {
        Task<ResponseModel> BillPayment(string accountNumber, BillDto bill);
        Task<TransactionDto> GetStatus(string TransactionRef);
        Task<ResponseModel> Transfer(string AccountNumber, string AccountNumberToTransfer, int AmountToTransfer);

        Task<ResponseModel> TransactionResponse(string Status, string TransactionType , string Message, Transaction transaction, HttpStatusCode code);
    }
}
