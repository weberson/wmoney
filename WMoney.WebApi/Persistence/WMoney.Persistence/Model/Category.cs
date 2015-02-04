using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMoney.Persistence.Model
{
    public class Category
    {
        public int CategoryId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
