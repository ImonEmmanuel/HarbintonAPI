using System.ComponentModel.DataAnnotations;

namespace Harbinton.API.Domain.Model
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public string TransactionRef { get; set; } = Generate();
        public string TransactionStatus { get; set; }
        public string TransactionType { get; set; }
        public string AccountNumber { get; set; }
        public int Amount { get; set; }

        private static string Generate()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);

            return rand.Next(100000000, 999999999).ToString();
        }
    }
}
