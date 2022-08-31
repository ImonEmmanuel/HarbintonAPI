using Harbinton.API.Model;
using Microsoft.EntityFrameworkCore;

namespace Harbinton.API.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(InitializeData.LoadUserData());
            modelBuilder.Entity<Account>().HasData(InitializeData.LoadAccoutData());

            base.OnModelCreating(modelBuilder);
        }



        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
