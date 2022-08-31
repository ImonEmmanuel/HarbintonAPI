namespace Harbinton.API.Dto
{
    public class DisplayDetailsDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public AccountDto Account {get;set;}
    }
}
