using System.ComponentModel.DataAnnotations;

namespace Harbinton.API.Dto
{
    public class AccountDto
    {
        //public int Id { get; set; }
        public int Amount { get; set; }

        [Required]
        public string AccountNumber { get; set; }

    }
}
