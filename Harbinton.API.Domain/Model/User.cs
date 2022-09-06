using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Harbinton.API.Domain.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        public DateTime DateUpdated { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address { get; set; }

        [Required, MinLength(11), MaxLength(11)]
        [Phone(ErrorMessage = "Please enter a valid phone number. with a Lenght of 11")]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [ForeignKey("AccountId")]
        public Account Account { get; set; }
        public int AccountId { get; set; }

    }
}
