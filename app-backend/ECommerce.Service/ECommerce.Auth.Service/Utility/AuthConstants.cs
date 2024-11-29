using System.ComponentModel;

namespace ECommerce.Auth.Service.Utility
{
    public static class AuthConstants
    {
        #region User
        // Constants
        public static readonly int Iterations = 3;
        public static readonly int MemorySize = 16384;
        public static readonly int Degree = 2;

        // Enums
        public enum ValidateUserModel
        {
            [Description("Email")]
            Email,
            [Description("PhoneNumber")]
            PhoneNumber,
            [Description("Name")]
            Name,
            [Description("password")]
            Password
        }

        // Error
        public static readonly string MandatoryFieldMissError = "Mandatory fields are missing - {0}";
        #endregion

    }
}
