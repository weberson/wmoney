using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMoney.Persistence.Model;
using WMoney.Persistence.Repositories;

namespace WMoney.Persistence.EntityFramework.Repositories
{
    /// <summary>
    /// User entity repository
    /// </summary>
    public class UserRepository : RepositoryBase<User, int>, IUserRepository
    {
        /// <summary>
        /// Initializes a new repository instance
        /// </summary>
        /// <param name="context"></param>	
        public UserRepository(WMoneyContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Obtains a User instance by its id
        /// </summary>
        /// <param name="id">User identifier</param>	
        public override User GetByID(int id, params string[] includeElements)
        {
            var userSet = Context.Users;

            foreach (var includeElement in includeElements)
            {
                userSet.Include(includeElement);
            }

            return userSet.Find(id);
        }
    }
}
