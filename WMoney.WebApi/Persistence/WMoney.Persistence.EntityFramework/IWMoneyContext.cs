using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMoney.Persistence.Repositories;

namespace WMoney.Persistence.EntityFramework
{
    public interface IWMoneyContext
    {
        IUserRepository UserRepository { get; }
    }
}
