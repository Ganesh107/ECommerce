using ECommerce.Product.Service.Utility;

namespace ECommerce.Product.Service.Model
{
    public class ProductModel : EntityBase
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Auto-generate a unique ID
        public string? ProductName { get; set; } 
        public string? Brand { get; set; } 
        public string? ModelNumber { get; set; } 
        public string? ModelName { get; set; } 
        public decimal? CurrentPrice { get; set; }
        public decimal? OriginalPrice { get; set; }
        public string? Memory { get; set; } 
        public string? Category { get; set; } 
        public string? SubCategory { get; set; } 
        public string? Color { get; set; } 
        public int? Count { get; set; }
        public string? Size { get; set; } 
        public IEnumerable<string> Images { get; set; } = [];
        public OverviewModel OverViewModel { get; set; } = new OverviewModel();
        public string? SellerName { get; set; } 
        public double? SellerRating { get; set; }
        public RatingAndReviews RatingAndReviews { get; set; } = new RatingAndReviews();
    }

    public class OverviewModel
    {
        public string? HighLights { get; set; } 
        public string? Overview { get; set; } 
        public string? Specifications { get; set; } 
    }

    public class RatingAndReviews
    {
        public double? OverallRating { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }

    public class Comment
    {
        public string UserName { get; set; } = string.Empty;
        public string? Text { get; set; } 
        public DateTime Date { get; set; } 
        public double Rating { get; set; }
    }
}