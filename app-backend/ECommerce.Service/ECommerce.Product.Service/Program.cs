
using ECommerce.Product.Service.Service;
using ECommerce.Product.Service.ProductContext;
using ECommerce.User.Service.Utility;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using MongoDB.Driver;

namespace ECommerce.Product.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigurationItem configItem = new();
            builder.Configuration.GetSection("ConfigurationItem").Bind(configItem);

            // Add services to the container.
            builder.Services.Configure<ConfigurationItem>(builder.Configuration.GetSection("ConfigurationItem"));
            
            builder.Services.AddControllers();

            // SQL Server
            builder.Services.AddDbContext<ProductDbContext>(options =>
                options.UseSqlServer(configItem.UserConnectionString));

            // MongoDB
            builder.Services.AddSingleton<IMongoClient>(sp =>
            {
                return new MongoClient(configItem.MongoDbConnectionString);
            });

            builder.Services.AddSingleton(options =>
            {
                var client = options.GetRequiredService<IMongoClient>();
                return client.GetDatabase("ECommerceDB");
            });

            // Service
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IProductService, ProductService>();

            // Configure CORS Policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("ECommerceCorsPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("ECommerceCorsPolicy");

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
