using AutoMapper;
using Harbinton.API.Database;
using Harbinton.API.Dto;
using Harbinton.API.Model;
using Harbinton.API.ResponseData;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Harbinton.API.Contract.Implementation
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public TransactionRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TransactionDto> GetStatus(string TransactionRef)
        {
            Transaction trans = await _context.Transactions.Where(x => x.TransactionRef == TransactionRef).FirstOrDefaultAsync();
            return _mapper.Map<TransactionDto>(trans);
        }

        public async Task<ResponseModel> BillPayment(string accountNumber, BillDto bill)
        {
            Account accountFrom = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);
            Transaction transact = new Transaction();
            transact.Amount = bill.Amount;
            transact.AccountNumber = bill.AccountNumber;

            if (accountFrom == null)
            {
                return await TransactionResponse("Failed", bill.BillType,
                    "Account to Initiate Transaction Failed", transact, HttpStatusCode.NotFound);
            }

            accountFrom.Amount = --bill.Amount;
            await _context.SaveChangesAsync();
            return await TransactionResponse("Success", bill.BillType,
                    "Transaction was SuccessFul", transact, HttpStatusCode.OK);
        }

        public async Task<ResponseModel> Transfer(string AccountNumber, string AccountNumberToTransfer, int AmountToTransfer)
        {
            Transaction transact = new Transaction();
            transact.Amount = 0;

            Account accountFrom = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == AccountNumber);
            Account accountTo = await _context.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == AccountNumberToTransfer);

            if (accountFrom == null)
            {
                return await TransactionResponse("Failed","Transfer", 
                    "Account Not Found! Enter Valid Account Number", transact, HttpStatusCode.NotFound);
            }
            
            transact.Amount = AmountToTransfer;
            transact.AccountNumber = AccountNumber;
            if (accountFrom.Amount < AmountToTransfer)
            {
                return await TransactionResponse("Failed", "Transfer",
                    "Insufficient Balance", transact, HttpStatusCode.Forbidden);
            }

            if (accountTo == null)
            {
                return await TransactionResponse("Failed", "Transfer",
                    "Receiver Account is Invalid Retry!", transact, HttpStatusCode.NotFound);
            }

            accountFrom.Amount = --AmountToTransfer;
            accountTo.Amount = ++AmountToTransfer;
            await _context.SaveChangesAsync();

            return await TransactionResponse("Success", "Transfer",
                    "Transfer Was Successful", transact, HttpStatusCode.OK);
        }

        public async Task<ResponseModel> TransactionResponse(string Status, string TransactionType, string Message, 
            Transaction transaction, HttpStatusCode code)
        {
            
            transaction.TransactionType = TransactionType;
            transaction.TransactionStatus = Status;
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            return new ResponseModel()
            {
                Message = Message,
                Status = transaction.TransactionStatus,
                TransactionRef = transaction.TransactionRef,
                ResponseCode = (int)code
            };
        }
    }
}
