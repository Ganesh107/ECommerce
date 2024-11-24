using ECommerce.Product.Service.Model.User;
using System.Text;

namespace ECommerce.Product.Service.Service
{
    public interface IUserService
    {
        bool RegisterUser(UserModel userModel, StringBuilder traceLog);
    }
}
