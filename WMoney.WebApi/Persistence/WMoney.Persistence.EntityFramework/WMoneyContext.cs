using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMoney.Persistence.Model;

namespace WMoney.Persistence.EntityFramework
{
    public class WMoneyContext : DbContext
    {
        public WMoneyContext()
            : base()
        { 
        
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
