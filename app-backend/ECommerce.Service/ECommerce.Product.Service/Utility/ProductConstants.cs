using System.ComponentModel;

namespace ECommerce.Product.Service.Utility
{
    public static class ProductConstants
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

        #region Blob 
        // Endpoints
        public static readonly string AddBlob = "Blob/AddBlob";
        public static readonly string Extension = ".jpg";

        // Error
        public static readonly string FileUploadError = "Failed to upload images in blob";
        public static readonly string AddBlobError = "Error occured while uploading in blob, got IsSuccessStatusCode as false";
        #endregion
    }
}
