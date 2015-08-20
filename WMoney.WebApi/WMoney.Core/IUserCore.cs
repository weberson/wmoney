using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMoney.Persistence.Model;

namespace WMoney.Core
{
    public interface IUserCore
    {
        Task<User> CreateUserAsync(string email, string password);

        Task<bool> CheckUserAsync(string email, string password);
    }
}
