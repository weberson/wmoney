using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMoney.Persistence.Model
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        public int TransactionTypeId { get; set; }

        public DateTime Created { get; set; }

        public DateTime Date { get; set; }

        public int CategoryId { get; set; }

        public int AccountId { get; set; }

        public decimal Value { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public virtual TransactionType TransactionType { get; set; }

        public virtual Category Category { get; set; }

        public virtual Account Account { get; set; }
    }
}
