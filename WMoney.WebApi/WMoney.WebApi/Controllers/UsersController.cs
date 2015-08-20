using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WMoney.Core;

namespace WMoney.WebApi.Controllers
{
    public class UsersController : ApiController
    {
        private IUserCore _userCore;

        public UsersController(IUserCore userCore)
        {
            _userCore = userCore;
        }

        public async Task Post(string email, string password)
        {
            try
            {
                await _userCore.CreateUserAsync(email, password);
            }
            catch (DuplicateWaitObjectException)
            {
                throw new HttpResponseException(HttpStatusCode.Ambiguous);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        public async Task<bool> Get(string email, string password)
        {
            try
            {
                return await _userCore.CheckUserAsync(email, password);
            }
            catch (ArgumentException)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
