using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMoney.Persistence.Model;

namespace WMoney.Core
{
    public class UserCore : IUserCore
    {
        public Task<User> CreateUserAsync(string email, string password)
        { 
            
        }
    }
}
