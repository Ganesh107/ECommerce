namespace ECommerce.Auth.Service.Utility
{
    public class ConfigurationItem
    {
        public string? UserConnectionString { get; set; }
        public string? KeyVaultUri { get; set; }
        public string? JwtKeyName { get; set; }
        public string? Issuer {  get; set; }
        public string? Audience { get; set; }
        public int TokenExpiryTime { get; set; }
        public string? JwtKey {  get; set; }

        public ConfigurationItem()
        {
            JwtKey = Helper.RetreieveJwtKey();
        }
    }
}
