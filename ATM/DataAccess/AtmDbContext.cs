using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public partial class AtmDbContext : DbContext
    {
        public AtmDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var users = new List<User>
            {
                new User
                {
                    
                },
                new User
                {
                    
                }
            };

            var accounts = new List<Account>
            {
                new Account
                {
                    
                },
                new Account
                {
                   
                },
            };

            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    
                },
                new Transaction
                {
                }
            };

            var cards = new List<Card>
            {
                new Card
                {
                    
                },
                new Card
                {
                }
            };
            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Account>().HasData(accounts);
            modelBuilder.Entity<Card>().HasData(cards);
            modelBuilder.Entity<Transaction>().HasData(transactions);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=DataSource=/Users/nikita/Documents/GitHub/ATMProject/ATM/DataAccess/atm.db");
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}