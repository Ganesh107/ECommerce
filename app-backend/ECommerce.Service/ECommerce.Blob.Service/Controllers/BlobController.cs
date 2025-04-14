using ECommerce.Blob.Service.Model;
using ECommerce.Blob.Service.Service;
using ECommerce.Blob.Service.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ECommerce.Blob.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlobController(IBlobService blobService) : ControllerBase
    {
        [Route("AddProduct")]
        [HttpPost]
        public HttpSingleReponseItem<bool> AddBlob(DocumentItem documentItem)
        {
            HttpSingleReponseItem<bool> response = new();
            StringBuilder traceLog = new();
            traceLog.Append("Started AddProduct method in user controller");
            bool data;
            try
            {
                data = blobService.AddBlob(documentItem, traceLog);
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
    }
}
