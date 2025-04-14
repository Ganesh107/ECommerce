using ECommerce.Product.Service.Model;
using MongoDB.Driver;
using System.Text;

namespace ECommerce.Product.Service.Service
{
    public class ProductService(IMongoClient _mongoClient) : IProductService
    {
        public readonly IMongoClient mongoClient = _mongoClient;
        private readonly IMongoDatabase _database = _mongoClient.GetDatabase("EcommerceDB");

        public bool AddProduct(ProductModel productDetail, StringBuilder traceLog)
        {   
            throw new NotImplementedException();
        }
    }
}
