namespace ECommerce.Auth.Service.Utility
{
    public class HttpSingleReponseItem<T>
    {
        public T? Data { get; set; }
        public int? StatusCode { get; set; }
        public string? Exception {  get; set; }
    }
}
