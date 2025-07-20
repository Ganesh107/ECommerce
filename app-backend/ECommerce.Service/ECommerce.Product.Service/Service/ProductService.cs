using ECommerce.Product.Service.Model;
using ECommerce.Product.Service.ProductContext;
using ECommerce.Product.Service.Utility;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Text;

namespace ECommerce.Product.Service.Service
{
    public class ProductService(IMongoDatabase database, IOptions<ConfigurationItem> configValues, ICacheService cacheService) : IProductService
    {
        #region Private Variables
        private readonly ConfigurationItem configItem = configValues.Value;
        private readonly HttpClient httpClient = new();
        private readonly ICacheService cacheService = cacheService;
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
            var productCollection = database.GetCollection<ProductModel>("Products");
            productDetail.Id = Guid.NewGuid().ToString();
            productDetail.CreatedBy = "system";
            productDetail.CreatedDate = DateTime.Now;
            productDetail.IsActive = true;

            // Upload Images in Blob
            List<DocumentItem> imagesToUpload = PrepareBlobUploadPayload(productDetail);
            UploadProductImagesInBlob(imagesToUpload, traceLog);

            // Add product in db
            productCollection.InsertOne(productDetail);

            // Event Based Cache Invalidation
            if (cacheService.KeyExistsAsync(ProductConstants.AllProducts).Result)
            {
                cacheService.RemoveAsync(ProductConstants.AllProducts).Wait();
                traceLog.Append("Cache cleared for all products.###");
            }

            isAdded = true;

            traceLog.Append("Exit From AddProduct Method");
            return isAdded;
        }

        /// <summary>
        /// Retrieves all products from the database.  
        /// </summary>  
        /// <param name="traceLog">  
        /// A <see cref="StringBuilder"/> instance used for logging the execution flow of the method.  
        /// </param>  
        /// <returns>  
        /// An <see cref="IEnumerable{ProductModel}"/> containing all products from the "Products" collection.  
        /// </returns>  
        public IEnumerable<ProductModel> GetProducts(StringBuilder traceLog)
        {
            traceLog.Append("Started GetProducts Service Method.###");
            if(cacheService.KeyExistsAsync(ProductConstants.AllProducts).Result)
            {
                traceLog.Append("Products found in cache.###");
                string cachedData = cacheService.GetAsync(ProductConstants.AllProducts).Result ?? string.Empty;
                if (!string.IsNullOrEmpty(cachedData))
                {
                    traceLog.Append("Returning cached products.###");
                    return JsonConvert.DeserializeObject<IEnumerable<ProductModel>>(cachedData) ?? [];
                }
            }

            var productCollection = database.GetCollection<ProductModel>("Products");
            var products = productCollection.Find(_ => true).ToList();

            // Add to Redis Cache
            if (products.Count > 0)
            {
                string productData = JsonConvert.SerializeObject(products);
                cacheService.SetAsync(ProductConstants.AllProducts, productData).Wait();
                traceLog.Append("Products added to cache.###");
            }
    
            traceLog.Append("Exit From GetProducts Service Method.###");
            return products;
        }

        /// <summary>
        /// Get Product By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="traceLog"></param>
        /// <returns></returns>
        public ProductModel GetProductById(string id, StringBuilder traceLog)
        {
            traceLog.Append("Started GetProductById Service Method.###");
            string cachedData = cacheService.GetAsync(ProductConstants.AllProducts).Result ?? string.Empty;
            if (!string.IsNullOrEmpty(cachedData))
            {
                traceLog.Append("Product found in cache.###");
                var productList = JsonConvert.DeserializeObject<IEnumerable<ProductModel>>(cachedData) ?? [];
                return productList.FirstOrDefault(p => p.Id == id) ?? new();
            }

            var productCollection = database.GetCollection<ProductModel>("Products");
            var product = productCollection.Find(p => p.Id == id).FirstOrDefault();
            traceLog.Append("Exit From GetProductById Service Method.###");
            return product;
        }

        /// <summary>
        /// Get Products By Category
        /// </summary>
        /// <param name="category"></param>
        /// <param name="traceLog"></param>
        /// <returns></returns>
        public List<ProductModel> GetProductsByCategory(string category, StringBuilder traceLog)
        {
            traceLog.Append("Started GetProductsByCategory Service Method.###");
            string cachedData = cacheService.GetAsync(ProductConstants.AllProducts).Result ?? string.Empty;
            if (!string.IsNullOrEmpty(cachedData))
            {
                traceLog.Append("Product found in cache.###");
                var productList = JsonConvert.DeserializeObject<IEnumerable<ProductModel>>(cachedData) ?? [];
                return [.. productList.Where(p => p.Category == category)];
            }

            var productCollection = database.GetCollection<ProductModel>("Products");
            var products = productCollection.Find(p => p.Category == category).ToList();
            traceLog.Append("Exit From GetProductsByCategory Service Method.###");
            return products;
        }

        #region Private Methods
        /// <summary>
        /// Prepare Blob Upload Payload
        /// </summary>
        /// <param name="productDetail"></param>
        /// <returns></returns>
        private List<DocumentItem> PrepareBlobUploadPayload(ProductModel productDetail)
        {
            List<DocumentItem> items = [];
            for (int i = 0; i < productDetail.Images.Length; i++)
            {
                items.Add(new()
                {
                    FileName = productDetail.ProductName,
                    Path = string.Format("{0}/{1}_{2}{3}", productDetail.Id, "Image", i + 1, ProductConstants.Extension),
                    FileBytes = productDetail.Images[i]
                });
                string imageUrl = string.Format("{0}/{1}", configItem.ECommerceBlobUrl, items[^1].Path);
                productDetail.ImageUrls.Add(imageUrl);
            }
            return items;
        }

        /// <summary>
        /// Upload Product Images In Blob
        /// </summary>
        /// <param name="documentItem"></param>
        /// <param name="traceLog"></param>
        /// <returns></returns>
        private bool UploadProductImagesInBlob(List<DocumentItem> documentItem, StringBuilder traceLog)
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
        #endregion
    }
}
