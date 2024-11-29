namespace ECommerce.Auth.Service.Model
{
    public class AuthRequest
    {
        public Guid? UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public byte[]? Hash { get; set; }
        public byte[]? Salt { get; set; }
    }
}
