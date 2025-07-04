using ECommerce.Product.Service.Model;
using ECommerce.Product.Service.Service;
using ECommerce.Product.Service.Utility;
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
                response.Data = data.ToList();
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
    }

}
