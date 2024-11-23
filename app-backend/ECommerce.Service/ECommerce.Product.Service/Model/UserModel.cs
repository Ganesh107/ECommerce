namespace ECommerce.User.Service.Model
{
    public class UserModel
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
