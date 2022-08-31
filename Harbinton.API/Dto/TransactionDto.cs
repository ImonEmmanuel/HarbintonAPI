using System.ComponentModel.DataAnnotations;

namespace Harbinton.API.Dto
{
    public class TransactionDto : AccountDto
    {
        public int TransactionRef { get; set; }
        public string TransactionStatus { get; set; }
        public string TransactionType { get; set; }
    }

}
