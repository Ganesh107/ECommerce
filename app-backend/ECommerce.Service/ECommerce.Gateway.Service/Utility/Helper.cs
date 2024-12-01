using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace ECommerce.Gateway.Service.Utility
{
    public static class Helper
    {
        /// <summary>
        /// Fetch JWT key from azure key vault
        /// </summary>
        /// <returns></returns>
        public static string RetreieveJwtKey()
        {
            var client = new SecretClient(new(GatewayConstants.KeyVaultUri), new DefaultAzureCredential());
            var fetch = Task.Run(() => client.GetSecretAsync(GatewayConstants.secretName));
            fetch.Wait();
            return fetch.Result.Value.Value;
        }
    }
}
