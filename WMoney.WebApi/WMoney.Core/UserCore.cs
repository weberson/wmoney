using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMoney.Persistence.EntityFramework;
using WMoney.Persistence.EntityFramework.Repositories;
using WMoney.Persistence.Model;
using WMoney.Persistence.Repositories;
using WMoney.Persistence.Filter;

namespace WMoney.Core
{
    public class UserCore : IUserCore
    {
        public IUserRepository _userRepository;

        public UserCore(IWMoneyContext wMoneyContext)
        {
            _userRepository = wMoneyContext.UserRepository;
        }

        public async Task<User> CreateUserAsync(string email, string password)
        {
            var existentUser = await _userRepository.AsQueryable().GetByEmail(email);

            if (existentUser != null)
            {
                throw new DuplicateWaitObjectException();
            }

            var user = new User 
            { 
                Email = email,
                Password = password
            };

            await _userRepository.AddAsync(user, true);

            return user;
        }

        public async Task<bool> CheckUserAsync(string email, string password)
        {
            var user = await _userRepository.AsQueryable().GetByEmail(email);

            if (user != null)
            {
                if (user.Password == password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new ArgumentException(string.Format("There's no user with email '{0}'", email));
            }
        }
    }
}
