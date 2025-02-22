
using ECommerce.Product.Service.Service;
using ECommerce.Product.Service.ProductContext;
using ECommerce.User.Service.Utility;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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

            // Context
            builder.Services.AddDbContext<ProductDbContext>(options =>
                options.UseSqlServer(configItem.UserConnectionString));
           
            // Service
            builder.Services.AddScoped<IUserService, UserService>();

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
            
            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
