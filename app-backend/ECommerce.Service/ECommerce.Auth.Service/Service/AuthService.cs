using ECommerce.Auth.Service.AuthDbContext;
using ECommerce.Auth.Service.Model;
using ECommerce.Auth.Service.Utility;
using Konscious.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ECommerce.Auth.Service.Service
{
    public class AuthService : IAuthService
    {
        private readonly AuthContext authContext;
        private readonly ConfigurationItem configItem;
        public AuthService(AuthContext authDbContext, IOptions<ConfigurationItem> configurationItem) 
        {
            authContext = authDbContext;
            configItem = configurationItem.Value;
        }

        #region Public Methods
        /// <summary>
        /// Authorize User
        /// </summary>
        /// <param name="authRequest"></param>
        /// <param name="traceLog"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public AuthResponse AuthorizeUser(AuthRequest authRequest, StringBuilder traceLog)
        {
            traceLog.Append("Started AuthorizeUser method in auth service ###.");
            ValidateAuthRequest(authRequest, traceLog);
            AuthResponse authResponse = new();

            // Validate user
            var user = authContext.Users.AsNoTracking().SingleOrDefault(u => u.Email!.Equals(authRequest.Email) 
                       || u.PhoneNumber!.Equals(authRequest.PhoneNumber)) 
                       ?? throw new Exception("User does not exist");

            // Verify credentials
            byte[] hash = GenerateHash(authRequest, user ?? new(), traceLog);
            if (!hash.SequenceEqual(user?.Hash ?? []))
            {
                throw new Exception("Invalid Credentials");
            }

            // Create access and refresh token
            authResponse.AccessToken = CreateAccessToken(user, traceLog);
            authResponse.RefreshToken = GenerateRefreshToken();

            traceLog.Append("Exit from AuthorizeUser method in auth service ###.");
            return authResponse;
        }

        /// <summary>
        /// Refresh Token
        /// </summary>
        /// <param name="authRequest"></param>
        /// <param name="traceLog"></param>
        /// <returns></returns>
        public AuthResponse RefreshToken(AuthResponse authRequest, HttpRequest httpRequest, StringBuilder traceLog)
        {
            traceLog.Append("Started RefreshToken method in auth service ###.");   
            if (string.IsNullOrEmpty(Helper.GetRefreshTokenFromCookies(httpRequest)))
            {
                throw new UnauthorizedAccessException("Unauthorized! please login again..");
            }

            AuthResponse authResponse = new();
            AuthRequest request = new();
            var handler = new JwtSecurityTokenHandler();
            var jsonToken =  handler.ReadJwtToken(authRequest.AccessToken) as JwtSecurityToken;

            // Read claims from access token
            request.Name = jsonToken?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            request.Email = jsonToken?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.UniqueName)?.Value;

            // Issue new access token and refresh token
            authResponse.AccessToken = CreateAccessToken(request, traceLog);
            authResponse.RefreshToken = GenerateRefreshToken();

            traceLog.Append("Exit from RefreshToken method in auth service ###.");
            return authResponse;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Create Access Token
        /// </summary>
        /// <param name="user"></param>
        /// <param name="traceLog"></param>
        /// <returns></returns>
        private string CreateAccessToken(AuthRequest? user, StringBuilder traceLog)
        {
            traceLog.Append("Started CreateAccessToken method in auth service ###.");
            // Set claims
            Claim[] claims = [
                new(JwtRegisteredClaimNames.Sub, user?.Name ?? string.Empty),
                new(JwtRegisteredClaimNames.UniqueName, user?.Email ?? string.Empty),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            ];

            // Token configuration
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configItem.JwtKey!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var accessToken = new JwtSecurityToken(
               issuer: configItem.Issuer,
               audience: configItem.Audience,
               claims: claims,
               expires: DateTime.UtcNow.AddMinutes(configItem.TokenExpiryTime),
               signingCredentials: creds
            );

            traceLog.Append("Exit from CreateAccessToken method in auth service ###.");
            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }

        /// <summary>
        /// Generate Refresh Token
        /// </summary>
        /// <returns></returns>
        private static string GenerateRefreshToken() 
            => Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        /// <summary>
        /// Generate Hash
        /// </summary>
        /// <param name="authRequest"></param>
        /// <param name="user"></param>
        /// <param name="traceLog"></param>
        /// <returns></returns>
        private static byte[] GenerateHash(AuthRequest authRequest, AuthRequest user, StringBuilder traceLog)
        {
            traceLog.Append("Started GenerateHash method in auth Service ###.");
            Argon2id argon2 = new(Encoding.UTF8.GetBytes(authRequest.Password ?? string.Empty))
            {
                Salt = user.Salt,
                Iterations = AuthConstants.Iterations,
                MemorySize = AuthConstants.MemorySize,
                DegreeOfParallelism = AuthConstants.Degree
            };
            traceLog.Append("Exit from GenerateHash method in auth Service ###.");
            return argon2.GetBytes(32);
        }

        /// <summary>
        /// Validate Auth Request
        /// </summary>
        /// <param name="authRequest"></param>
        /// <param name="traceLog"></param>
        /// <exception cref="ArgumentException"></exception>
        private static void ValidateAuthRequest(AuthRequest authRequest, StringBuilder traceLog)
        {
            traceLog.Append("ValidateAuthRequest method started in auth Service ###.");
            StringBuilder errorLog = new();
            errorLog.Append(!string.IsNullOrEmpty(authRequest.Email) || !string.IsNullOrEmpty(authRequest.PhoneNumber) ? string.Empty
                : Helper.GetDescription(AuthConstants.ValidateUserModel.Email) + " or " + 
                Helper.GetDescription(AuthConstants.ValidateUserModel.PhoneNumber)).AppendLine();
            errorLog.Append(!string.IsNullOrEmpty(authRequest.Password) ? string.Empty
                : Helper.GetDescription(AuthConstants.ValidateUserModel.Password)).AppendLine();

            string errorMessage = string.Join(",", errorLog.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new ArgumentException(string.Format(AuthConstants.MandatoryFieldMissError, errorMessage));
            }
            traceLog.Append("Exit from ValidateAuthRequest method started in auth service ###.");
        }
        #endregion
    }
}
