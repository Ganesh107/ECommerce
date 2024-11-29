using ECommerce.Product.Service.Model.User;
using ECommerce.Product.Service.ProductContext;
using System.Security.Cryptography;
using Konscious.Security.Cryptography;
using System.Text;
using ECommerce.Product.Service.Utility;

namespace ECommerce.Product.Service.Service
{
    public class UserService : IUserService
    {
        private readonly ProductDbContext productContext;
        public UserService(ProductDbContext productDbContext)
        {
            productContext = productDbContext;  
        }

        #region Public Methods
        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="traceLog"></param>
        /// <returns></returns>
        public bool RegisterUser(UserModel user, StringBuilder traceLog)
        {
            traceLog.Append("RegisterUser method started in user Service ###."); 
            ValidateUserModel(user, traceLog);

            if (productContext.ECommerceUsers.Any(u => u.Email == user.Email || u.PhoneNumber == user.PhoneNumber))
            {
                throw new Exception("User already registered!");
            }   
           
            // Assign unique id and created date
            user.UserId = Guid.NewGuid();
            user.CreatedDate = DateTime.Now;

            // Hash the password
            Hashpassword(user, traceLog);

            // Add user to table
            productContext.ECommerceUsers.Add(user);
            productContext.SaveChanges();

            traceLog.Append("Exit from RegisterUser method started in user service ###.");
            return true;    
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Hash password
        /// </summary>
        /// <param name="user"></param>
        /// <param name="traceLog"></param>
        private static void Hashpassword(UserModel user, StringBuilder traceLog)
        {
            traceLog.Append("Hashpassword method started in user Service ###.");
            byte[] salt = RandomNumberGenerator.GetBytes(32);
            user.Salt = salt;
            Argon2id argon2 = new (Encoding.UTF8.GetBytes(user.Password ?? string.Empty))
            {
                Salt = salt,
                Iterations = ProductConstants.Iterations,
                MemorySize = ProductConstants.MemorySize,
                DegreeOfParallelism = ProductConstants.Degree
            };
            user.Hash = argon2.GetBytes(32);
            traceLog.Append("Exit from Hashpassword method started in user service ###.");
        }

        /// <summary>
        /// Validate User Model
        /// </summary>
        /// <param name="user"></param>
        /// <param name="traceLog"></param>
        /// <exception cref="ArgumentException"></exception>
        private static void ValidateUserModel(UserModel user, StringBuilder traceLog)
        {
            traceLog.Append("ValidateUserModel method started in user Service ###.");
            StringBuilder errorLog = new();
            errorLog.Append(!string.IsNullOrEmpty(user.Email) ? string.Empty
                : Helper.GetDescription(ProductConstants.ValidateUserModel.Email)).AppendLine();
            errorLog.Append(!string.IsNullOrEmpty(user.PhoneNumber) ? string.Empty
                : Helper.GetDescription(ProductConstants.ValidateUserModel.PhoneNumber)).AppendLine();
            errorLog.Append(!string.IsNullOrEmpty(user.Name) ? string.Empty
                : Helper.GetDescription(ProductConstants.ValidateUserModel.Name)).AppendLine();
            errorLog.Append(!string.IsNullOrEmpty(user.Password) ? string.Empty
                : Helper.GetDescription(ProductConstants.ValidateUserModel.Password)).AppendLine();

            string errorMessage = string.Join(",", errorLog.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
            if(!string.IsNullOrEmpty(errorMessage))
            {
                throw new ArgumentException(string.Format(ProductConstants.MandatoryFieldMissError, errorMessage));
            }

            traceLog.Append("Exit from ValidateUserModel method started in user service ###.");
        }
        #endregion
    }
}
