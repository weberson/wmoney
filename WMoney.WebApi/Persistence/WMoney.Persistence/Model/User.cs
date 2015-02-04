using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMoney.Persistence.Model
{
    public class User
    {
        public int UserId { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Password { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
