namespace ECommerce.Auth.Service.Utility
{
    public class HttpResponseItem<T>
    {
        public List<T>? Data { get; set; }
        public int? StatusCode { get; set; }
        public string? Exception {  get; set; }
    }
}
