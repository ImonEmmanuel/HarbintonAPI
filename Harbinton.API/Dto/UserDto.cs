using Harbinton.API.Database;
using Harbinton.API.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Harbinton.API.Dto
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
