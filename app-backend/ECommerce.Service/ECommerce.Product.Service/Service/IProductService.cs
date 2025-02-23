using ECommerce.Product.Service.Model;
using MongoDB.Bson;
using System.Text;

namespace ECommerce.Product.Service.Service
{
    public interface IProductService
    {
        public bool AddProduct(ProductModel productDetails, StringBuilder traceLog);
    }
}
