using ECommerce.Product.Service.Model;
using ECommerce.Product.Service.Service;
using ECommerce.Product.Service.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ECommerce.Product.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }

        [Route("AddProduct")]
        [HttpPost]
        public HttpSingleReponseItem<bool> AddProduct(ProductModel productDetail)
        {
            HttpSingleReponseItem<bool> response = new();
            StringBuilder traceLog = new();
            traceLog.Append("Started AddProduct method in user controller");
            bool data;
            try
            {
                data = productService.AddProduct(productDetail, traceLog);
                response.Data = data;
                response.StatusCode = 200;
            }
            catch (Exception exception)
            {
                response.StatusCode = 500;
                response.Exception = exception.Message;
            }
            traceLog.Append("Exit from AddProduct method in user controller");
            return response;
        }

        [Route("GetProducts")]
        [HttpGet]
        public HttpResponseItem<ProductModel> GetProducts()
        {
            HttpResponseItem<ProductModel> response = new();
            StringBuilder traceLog = new();
            traceLog.Append("Started AddProduct method in user controller");
            IEnumerable<ProductModel> data;
            try
            {
                data = productService.GetProducts(traceLog);
                response.Data = [.. data];
                response.StatusCode = 200;
            }
            catch (Exception exception)
            {
                response.StatusCode = 500;
                response.Exception = exception.Message;
            }
            traceLog.Append("Exit from AddProduct method in user controller");
            return response;
        }

        [Route("GetProductById/{id}")]
        [HttpGet]
        public HttpSingleReponseItem<ProductModel> GetProductById(string id)
        {
            HttpSingleReponseItem<ProductModel> response = new();
            StringBuilder traceLog = new();
            traceLog.Append("Started GetProductById method in user controller");
            ProductModel data;
            try
            {
                data = productService.GetProductById(id, traceLog);
                response.Data = data;
                response.StatusCode = 200;
            }
            catch (Exception exception)
            {
                response.StatusCode = 500;
                response.Exception = exception.Message;
            }
            traceLog.Append("Exit from GetProductById method in user controller");
            return response;
        }

        [Route("GetProductsByCategory/{category}")]
        [HttpGet]
        public HttpResponseItem<ProductModel> GetProductsByCategory(string category)
        {
            HttpResponseItem<ProductModel> response = new();
            StringBuilder traceLog = new();
            traceLog.Append("Started GetProductsByCategory method in user controller");
            IEnumerable<ProductModel> data;
            try
            {
                data = productService.GetProductsByCategory(category, traceLog);
                response.Data = [.. data];
                response.StatusCode = 200;
            }
            catch (Exception exception)
            {
                response.StatusCode = 500;
                response.Exception = exception.Message;
            }
            traceLog.Append("Exit from GetProductsByCategory method in user controller");
            return response;
        }
    }
}
