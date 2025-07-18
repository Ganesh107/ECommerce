using ECommerce.Product.Service.Model;
using MongoDB.Bson;
using System.Text;

namespace ECommerce.Product.Service.Service
{
    public interface IProductService
    {
        public bool AddProduct(ProductModel productDetails, StringBuilder traceLog);
        public IEnumerable<ProductModel> GetProducts(StringBuilder traceLog);
        public ProductModel GetProductById(string id, StringBuilder traceLog);
        public List<ProductModel> GetProductsByCategory(string category, StringBuilder traceLog);
    }
}
