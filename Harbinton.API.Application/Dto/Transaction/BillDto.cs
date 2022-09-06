using Harbinton.API.Application.Dto.User;
using System.ComponentModel.DataAnnotations;

namespace Harbinton.API.Application.Dto.Transaction
{
    public class BillDto : AccountDto
    {
        [Required]
        public string BillType { get; set; }
    }



}
