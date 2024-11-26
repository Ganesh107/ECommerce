namespace ECommerce.Auth.Service.Model
{
    public class AuthRequest
    {
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public byte[]? Hash { get; set; }
        public byte[]? Salt { get; set; }
    }
}
