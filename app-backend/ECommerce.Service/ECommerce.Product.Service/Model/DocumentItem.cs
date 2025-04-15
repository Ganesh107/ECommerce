namespace ECommerce.Product.Service.Model
{
    public class DocumentItem
    {
        public string? FileName { get; set; }
        public string? Path { get; set; }
        public List<string>? FileBytes { get; set; }
    }
}
