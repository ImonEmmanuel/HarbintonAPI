using Harbinton.API.Model;

namespace Harbinton.API.Database
{
    public static class InitializeData
    {
        public static IEnumerable<User> LoadUserData()
        {
            var defaultUserData = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    FirstName = "Emmanuel",
                    LastName = "Imonmion",
                    Address = "Iyana-Ipaja",
                    Phone = "08183836790",
                    AccountId = 1
                },

                new User()
                {
                    Id = 2,
                    FirstName = "Bright",
                    LastName = "Emmanuel",
                    Address = "Yaba",
                    Phone = "08164503345",
                    AccountId = 2
                }
            };

            return defaultUserData;
        }

        public static IEnumerable<Account> LoadAccoutData()
        {
            var defaultAccountData = new List<Account>()
            {
                new Account()
                {
                    Id = 1,
                },

                new Account()
                {
                    Id = 2,
                }
            };

            return defaultAccountData;
        }
    }
}
