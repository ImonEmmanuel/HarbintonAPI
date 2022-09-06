using System.ComponentModel.DataAnnotations.Schema;

namespace Harbinton.API.Application.Dto.User
{
    public class UserDto
    {
        public int AccountID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

    }
}
