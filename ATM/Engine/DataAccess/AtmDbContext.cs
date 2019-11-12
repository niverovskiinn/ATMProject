using Microsoft.EntityFrameworkCore;
using Models;
using Models.Enum;

namespace Engine.DataAccess
{
    public class AtmDbContext : DbContext
    {
        public AtmDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlite("Data Source=/Users/nikita/Documents/GitHub/ATMProject/ATM/Engine/DataAccess/AtmDb.db");
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountStatus> AccountStatuses { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }

        public DbSet<Card> Cards { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Add Entities to DB

//             List<Card> cards = new List<Card>
//            {
//                new Card
//                {
//                    Number = "1234123412341234",
//                    AccountId = 1
//                    
//                },
//                new Card
//                {
//                    Number = "1234123412341235",
//                    AccountId = 2
//                }
//            };
//
//             List<Account> accounts = new List<Account>
//            {
//                new Account
//                {
//                    Id = 1,
//                    OwnerPassport = "AA123456",
//
//                },
//                new Account
//                {
//                    Id = 2,
//                    OwnerPassport = "AA654321",
//                },
//            };
//
//             List<User> users = new List<User>            {
//                new User
//                {
//                    Passport = "AA123456",
//                    Accounts = accounts
//
//                },
//                new User
//                {
//                    Passport = "AA654321",
//                    Accounts = accounts
//                }
//            };
//            var transactions = new List<Transaction>
//            {
//                
//                new Transaction
//                {
//                    Id = 1,
//                    AccountFromId = 1,
//                    AccountToId = 2,
//                    From = accounts[0],
//                    To = accounts[1]
//
//                },
//                new Transaction
//                {
//                    Id = 2,
//                    AccountFromId = 1,
//                    From = accounts[0],
//                    To = null
//                }
//            };
//
//            
////            modelBuilder.Entity<User>().HasData(users);
////            modelBuilder.Entity<Account>().HasData(accounts);
////            modelBuilder.Entity<Card>().HasData(cards);
////            modelBuilder.Entity<Transaction>().HasData(transactions);

            #endregion

            modelBuilder.Entity<AccountStatus>().HasData(AccountStatusEnum.Active, AccountStatusEnum.Closed,
                AccountStatusEnum.Frozen);
            modelBuilder.Entity<AccountType>().HasData(AccountTypeEnum.Credit, AccountTypeEnum.Debit);
            modelBuilder.Entity<TransactionType>().HasData(TransactionTypeEnum.Withdraw, TransactionTypeEnum.ToUser, 
                TransactionTypeEnum.Deposit);

            base.OnModelCreating(modelBuilder);
        }
    }
}