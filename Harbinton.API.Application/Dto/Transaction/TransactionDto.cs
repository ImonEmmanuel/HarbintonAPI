using Harbinton.API.Application.Dto.User;
using System.ComponentModel.DataAnnotations;

namespace Harbinton.API.Application.Dto.Transaction

{
    public class TransactionDto : AccountDto
    {
        public int TransactionRef { get; set; }
        public string TransactionStatus { get; set; }
        public string TransactionType { get; set; }
    }

}
