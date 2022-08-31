using System.ComponentModel.DataAnnotations;

namespace Harbinton.API.Dto
{
    public class BillDto : AccountDto
    {
        [Required]
        public string BillType { get; set; }
    }



}
