using ECommerce.Auth.Service.Model;
using System.Text;

namespace ECommerce.Auth.Service.Service
{
    public interface IAuthService
    {
        AuthResponse AuthorizeUser(AuthRequest request, StringBuilder traceLog);
    }
}
