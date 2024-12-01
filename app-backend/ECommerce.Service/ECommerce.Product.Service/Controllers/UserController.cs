using ECommerce.Product.Service.Model.User;
using ECommerce.Product.Service.Service;
using ECommerce.Product.Service.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ECommerce.Product.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        #region Controller Methods
        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("RegisterUser")]
        [HttpPost]
        public HttpSingleReponseItem<bool> RegisterUser(UserModel user)
        {
            HttpSingleReponseItem<bool> response = new();
            StringBuilder traceLog = new();
            traceLog.Append("Started RegisterUser method in user controller");
            bool data;
            try
            {
                data = userService.RegisterUser(user, traceLog);
                response.Data = data;
                response.StatusCode = 200;
            }
            catch (Exception exception)
            {
                response.StatusCode = 500;
                response.Exception = exception.Message;
            }
            traceLog.Append("Exit from RegisterUser method in user controller");
            return response;
        }
        #endregion
    }
}
