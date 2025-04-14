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

        public bool AddBlob(DocumentItem documentItem, StringBuilder traceLog)
        {
            traceLog.Append("Started AddBlob Service Method.###");
            bool isUploaded;
            try
            {
                BlobServiceClient blobServiceClient = new(configItem.BlobConnectionString);
                BlobContainerClient containerClient = GetBlobContainerClient(blobServiceClient, traceLog);
                BlobClient blobClient = GetBlobClient(documentItem, containerClient, traceLog);
                isUploaded = UploadBlob(blobClient, documentItem, traceLog);
                traceLog.AppendFormat("Status Of Uploading File To Blob: {0}.###", isUploaded);
            }
            catch (Exception)
            {
                throw new("Error while uploading to blob");
            }

            traceLog.Append("Exit From AddBlob Service Method.###");
            return isUploaded;
        }

        private static bool UploadBlob(BlobClient blobClient, DocumentItem documentItem, StringBuilder traceLog)
        {
            traceLog.Append("Started UploadBlob Method");
            bool isUploaded = false;
            try
            {
                using var stream = new MemoryStream(documentItem.FileBytes ?? []);
                blobClient.UploadAsync(stream, overwrite: true).Wait();
                isUploaded = true;
            }
            catch (Exception)
            {
                throw new("Error while writing to blob");
            }
            traceLog.Append("Exit From UploadBlob Method");
            return isUploaded;
        }

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
