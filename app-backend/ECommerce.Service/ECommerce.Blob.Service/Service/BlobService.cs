using Azure.Storage.Blobs;
using ECommerce.Blob.Service.Model;
using ECommerce.Blob.Service.Utility;
using Microsoft.Extensions.Options;
using System.Text;

namespace ECommerce.Blob.Service.Service
{
    public class BlobService(IOptions<ConfigurationItem> configurationItem) : IBlobService
    {
        private readonly ConfigurationItem configItem = configurationItem.Value;

        /// <summary>
        /// AddBlob
        /// </summary>
        /// <param name="documentItem"></param>
        /// <param name="traceLog"></param>
        /// <returns></returns>
        public bool AddBlob(List<DocumentItem> documentItem, StringBuilder traceLog)
        {
            traceLog.Append("Started AddBlob Service Method.###");
            bool isUploaded;
            try
            {
                List<(BlobClient, DocumentItem)> uploadItems = [];
                BlobServiceClient blobServiceClient = new(configItem.BlobConnectionString);
                BlobContainerClient containerClient = GetBlobContainerClient(blobServiceClient, traceLog);

                foreach (var item in documentItem)
                {
                    BlobClient blobClient = GetBlobClient(item, containerClient, traceLog);
                    uploadItems.Add((blobClient, item));    
                }

                isUploaded = UploadBlob(uploadItems, traceLog).Result;
                traceLog.AppendFormat("Status Of Uploading File To Blob: {0}.###", isUploaded);
            }
            catch (Exception)
            {
                throw new("Error while uploading to blob");
            }
            traceLog.Append("Exit From AddBlob Service Method.###");
            return isUploaded;
        }

        /// <summary>
        /// Upload Blob
        /// </summary>
        /// <param name="blobClient"></param>
        /// <param name="documentItem"></param>
        /// <param name="traceLog"></param>
        /// <returns></returns>
        private static async Task<bool> UploadBlob(List<(BlobClient, DocumentItem)> uploadItems, StringBuilder traceLog)
        {
            traceLog.Append("Started UploadBlob Method");
            bool isUploaded;
            try
            {
                foreach (var item in uploadItems)
                {
                    var (client, uploadItem) = (item.Item1, item.Item2);
                    byte[] byteArray = Convert.FromBase64String(uploadItem.FileBytes ?? string.Empty);
                    using var stream = new MemoryStream(byteArray ?? []);
                    await client.UploadAsync(stream, overwrite: true);
                }
                isUploaded = true;
            }
            catch (Exception)
            {
                throw new("Error while writing to blob");
            }
            traceLog.Append("Exit From UploadBlob Method");
            return isUploaded;
        }

        /// <summary>
        /// Get Blob Client
        /// </summary>
        /// <param name="documentItem"></param>
        /// <param name="containerClient"></param>
        /// <param name="traceLog"></param>
        /// <returns></returns>
        private static BlobClient GetBlobClient(DocumentItem documentItem, BlobContainerClient containerClient, StringBuilder traceLog)
        {
            traceLog.Append("Started GetBlobClient Method");
            BlobClient blobClient;
            try
            {
                blobClient = containerClient.GetBlobClient(documentItem.Path);
            }
            catch (Exception)
            {
                throw new("Error while fetching document from blob");
            }
            traceLog.Append("Exit From GetBlobClient Method");
            return blobClient;
        }

        /// <summary>
        /// Get Blob Container Client
        /// </summary>
        /// <param name="blobClient"></param>
        /// <param name="traceLog"></param>
        /// <returns></returns>
        private BlobContainerClient GetBlobContainerClient(BlobServiceClient blobClient, StringBuilder traceLog)
        {
            traceLog.Append("Started GetBlobContainerClient Method");
            BlobContainerClient containerClient;
            try
            {
                containerClient = blobClient.GetBlobContainerClient(configItem.BlobContainer);
                containerClient.CreateIfNotExistsAsync().Wait();
            }
            catch (Exception)
            {
                throw new("Error while creating blob container");
            }
            traceLog.Append("Exit From GetBlobContainerClient Method");
            return containerClient;
        }
    }
}
