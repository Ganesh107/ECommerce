using ECommerce.Product.Service.Model;
using ECommerce.Product.Service.Utility;
using ECommerce.User.Service.Utility;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Text;

namespace ECommerce.Product.Service.Service
{
    public class ProductService(IMongoClient _mongoClient, IOptions<ConfigurationItem> configValues) : IProductService
    {
        #region Private Variables
        public readonly IMongoClient mongoClient = _mongoClient;
        private readonly IMongoDatabase database = _mongoClient.GetDatabase("EcommerceDB");
        private readonly ConfigurationItem configItem = configValues.Value;
        private readonly HttpClient httpClient = new();
        #endregion

        /// <summary>
        /// Add Product
        /// </summary>
        /// <param name="productDetail"></param>
        /// <param name="traceLog"></param>
        /// <returns></returns>
        public bool AddProduct(ProductModel productDetail, StringBuilder traceLog)
        {
            traceLog.Append("Started AddProduct Service Method.###");
            bool isAdded;
            var productCollection = database.GetCollection<ProductModel>("ECommerceProducts");
            ConstructBlobPath(productDetail, traceLog);
            DocumentItem documentItem = new()
            {
                FileName = productDetail.ProductName,
                Path = productDetail.Blobpath
            };

            foreach (var image in productDetail.Images)
            {
                documentItem.FileBytes?.Add(image);
            }

            UploadProductImagesInBlob(documentItem, traceLog);
            productCollection.InsertOne(productDetail);
            isAdded = true;

            traceLog.Append("Exit From AddProduct Method");
            return isAdded;
        }

        /// <summary>
        /// Create Blob For Each Content To Be Uploaded
        /// </summary>
        /// <param name="productDetail"></param>
        /// <param name="traceLog"></param>
        private void ConstructBlobPath(ProductModel productDetail, StringBuilder traceLog)
        {
            traceLog.Append("Started ConstructBlobPath Method.###");
            productDetail.Blobpath = string.Format("{0}/{1}", productDetail.Id, productDetail.ProductName);
            traceLog.Append("Exit From ConstructBlobPath Method");
        }

        /// <summary>
        /// Upload Product Images In Blob
        /// </summary>
        /// <param name="documentItem"></param>
        /// <param name="traceLog"></param>
        /// <returns></returns>
        private bool UploadProductImagesInBlob(DocumentItem documentItem, StringBuilder traceLog)
        {
            traceLog.Append("Started UploadProductImagesInBlob Service Method.###");
            bool isUploaded;
            string payload = JsonConvert.SerializeObject(documentItem);
            string url = string.Format("{0}/{1}", configItem.BlobServiceUrl, ProductConstants.AddBlob);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(url, content).Result;
            if (response != null && response.IsSuccessStatusCode)
            {
                string responseItem = response.Content.ReadAsStringAsync().Result;
                var httpReponseItem = JsonConvert.DeserializeObject<HttpSingleReponseItem<bool>>(responseItem);
                if (httpReponseItem != null && httpReponseItem.StatusCode == 200)
                {
                    isUploaded = httpReponseItem.Data;
                }
                else
                {
                    throw new(ProductConstants.FileUploadError);
                }
            }
            else
            {
                throw new(ProductConstants.AddBlobError);
            }
            traceLog.Append("Exit From UploadProductImagesInBlob Method");
            return isUploaded;
        }
    }
}
