using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMoney.Persistence.EntityFramework.Repositories;
using WMoney.Persistence.Model;
using WMoney.Persistence.Repositories;

namespace WMoney.Persistence.EntityFramework
{
    public class WMoneyContext : DbContext, IWMoneyContext
    {
        public WMoneyContext()
            : base("WMoneyConnectionString")
            {
            
            }

        public IUserRepository UserRepository
        { 
            get { return new UserRepository(this); }
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .HasKey(a => a.AccountId)
                .Map(a => a.ToTable("TbAccount", "dbo"));

            modelBuilder.Entity<Account>()
                .HasRequired(a => a.User)
                .WithMany(a => a.Accounts)
                .HasForeignKey(a => a.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasKey(a => a.CategoryId)
                .Map(a => a.ToTable("TbCategory", "dbo"));

            modelBuilder.Entity<Category>()
                .HasRequired(a => a.User)
                .WithMany(a => a.Categories)
                .HasForeignKey(a => a.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transaction>()
                .HasKey(a => a.TransactionId)
                .Map(a => a.ToTable("TbTransaction", "dbo"));

            modelBuilder.Entity<Transaction>()
                .HasRequired(a => a.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(a => a.AccountId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transaction>()
                .HasRequired(a => a.Category)
                .WithMany(a => a.Transactions)
                .HasForeignKey(a => a.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transaction>()
                .HasRequired(a => a.TransactionType)
                .WithMany(a => a.Transactions)
                .HasForeignKey(a => a.TransactionTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TransactionType>()
                .HasKey(a => a.TransactionTypeId)
                .Map(a => a.ToTable("TbTransactionType", "dbo"));

            modelBuilder.Entity<User>()
                .HasKey(a => a.UserId)
                .Map(a => a.ToTable("TbUser", "dbo"));
        }



    }
}
