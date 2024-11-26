using ECommerce.Auth.Service.Model;
using ECommerce.Auth.Service.Service;
using ECommerce.Auth.Service.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ECommerce.Auth.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService _authService)
        {    
            authService = _authService;
        }

        [Route("AuthorizeUser")]
        [HttpPost]
        public HttpSingleReponseItem<AuthResponse> AuthorizeUser(AuthRequest authRequest)
        {
            HttpSingleReponseItem<AuthResponse> response = new();
            StringBuilder traceLog = new();
            traceLog.Append("Started AuthorizeUser method in AuthorizeUser controller");
            try
            {
                response.Data = authService.AuthorizeUser(authRequest, traceLog);
                response.StatusCode = 200;
            }
            catch (Exception exception)
            {
                response.StatusCode = 500;
                response.Exception = exception.Message;
            }
            traceLog.Append("Exit from AuthorizeUser method in AuthorizeUser controller");
            return response;
        }
    }
}
