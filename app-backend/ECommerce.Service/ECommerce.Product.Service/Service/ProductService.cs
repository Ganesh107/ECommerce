using ECommerce.Product.Service.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text;

namespace ECommerce.Product.Service.Service
{
    public class ProductService : IProductService
    {
        public readonly IMongoClient mongoClient;
        private readonly IMongoDatabase _database;

        public ProductService(IMongoClient _mongoClient)
        {
            mongoClient = _mongoClient;
            _database = _mongoClient.GetDatabase("EcommerceDB");
        }
        public bool AddProduct(ProductModel productDetail, StringBuilder traceLog)
        { 
            throw new NotImplementedException();
        }
    }
}
