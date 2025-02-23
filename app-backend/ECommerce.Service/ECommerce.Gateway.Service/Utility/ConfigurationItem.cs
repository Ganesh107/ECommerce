namespace ECommerce.Gateway.Service.Utility
{
    public class ConfigurationItem
    {
        public string? KeyVaultUri { get; set; }
        public string? Issuer {  get; set; }
        public string? Audience { get; set; }
        public string? JwtKey {  get; set; }
    }
}
