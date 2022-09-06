using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Harbinton.API.Domain.Model
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; } = 0;
        public string AccountNumber { get; set; } = GetAcc();

        private static string GetAcc()
        {
            var acc = "3";
            Random rd = new Random();
            for (int i = 0; i < 9; i++)
            {
                string rand_num = rd.Next(0, 9).ToString();
                acc = acc + rand_num;
            }
            return acc;
        }
    }

   
}
