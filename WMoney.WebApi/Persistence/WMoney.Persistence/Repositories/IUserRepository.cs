using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMoney.Persistence.Model;

namespace WMoney.Persistence.Repositories
{
    /// <summary>
    /// Interface for User entity repository
    /// </summary>
    public interface IUserRepository : IEntityRepository<User, int>
    {
        
    }	
}
