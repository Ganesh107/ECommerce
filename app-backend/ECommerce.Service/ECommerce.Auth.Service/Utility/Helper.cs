using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System.ComponentModel;
using System.Reflection;

namespace ECommerce.Auth.Service.Utility
{
    public static class Helper
    {
        /// <summary>
        /// Get Description
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string GetDescription(Enum property)
        {
            FieldInfo field = property.GetType().GetField(property.ToString())!;
            DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>()!;
            return attribute?.Description ?? string.Empty;
        }

        /// <summary>
        /// Fetch JWT key from azure key vault
        /// </summary>
        /// <returns></returns>
        public static string RetreieveJwtKey()
        {
            var client = new SecretClient(new(AuthConstants.KeyVaultUri), new DefaultAzureCredential());
            var fetch = Task.Run(() => client.GetSecretAsync(AuthConstants.secretName));
            fetch.Wait();
            return fetch.Result.Value.Value;
        }

        /// <summary>
        /// Set Cookies
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <param name="refreshToken"></param>
        public static void SetCookies(HttpResponse httpResponse, string? refreshToken)
        {
            httpResponse.Cookies.Append("RefreshToken", refreshToken ?? string.Empty, new()
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddHours(1),
                SameSite = SameSiteMode.Strict,
                Path = "/api/Auth/"
            });
        }

        /// <summary>
        /// Get Refresh Token From Cookies
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public static string? GetRefreshTokenFromCookies(HttpRequest httpRequest)
        {
            if (httpRequest.Cookies.TryGetValue("RefreshToken", out string? token))
            {
                return token;
            }
            return string.Empty;    
        }
    }
}
