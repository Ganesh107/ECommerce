using ECommerce.Product.Service.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.Product.Service.Model
{
    public class ProductModel : EntityBase
    {
        [BsonId]
        public string Id { get; set; } = string.Empty; 
        public string? ProductName { get; set; } 
        public string? Brand { get; set; } 
        public string? ModelNumber { get; set; } 
        public string? ModelName { get; set; } 
        public string? CurrentPrice { get; set; }
        public string? OriginalPrice { get; set; }
        public string? Memory { get; set; } 
        public string? Category { get; set; } 
        public string? SubCategory { get; set; } 
        public string? Color { get; set; } 
        public int? Count { get; set; }
        public string? Size { get; set; }
        [BsonIgnore]
        public string[] Images { get; set; } = [];

        public List<string> ImageUrls = [];
        public OverviewModel OverViewModel { get; set; } = new();
        public string? SellerName { get; set; } 
        public double? SellerRating { get; set; }
        public RatingAndReviews RatingAndReviews { get; set; } = new();
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
        public List<Comment> Comments { get; set; } = [];
    }

    public class Comment
    {
        public string UserName { get; set; } = string.Empty;
        public string? Text { get; set; } 
        public DateTime Date { get; set; } 
        public double Rating { get; set; }
    }
}