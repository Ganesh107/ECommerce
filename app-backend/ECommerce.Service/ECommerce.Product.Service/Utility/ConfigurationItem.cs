namespace ECommerce.User.Service.Utility
{
    public class ConfigurationItem
    {
        public string? UserConnectionString { get; set; }
        public string? MongoDbConnectionString { get; set; }
        public string? BlobServiceUrl { get; set; }
    }
}
