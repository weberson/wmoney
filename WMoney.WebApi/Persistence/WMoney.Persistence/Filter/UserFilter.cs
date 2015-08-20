using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMoney.Persistence.Model;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace WMoney.Persistence.Filter
{
    public static class UserFilter
    {
        public static async Task<User> GetByEmail(this IQueryable<User> users, string email)
        {
            return await users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }
    }
}
