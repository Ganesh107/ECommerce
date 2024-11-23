
using ECommerce.User.Service.Model;
using ECommerce.User.Service.UserContext;
using ECommerce.User.Service.Utility;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.User.Service
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
            builder.Services.AddDbContext<UserDbContext>(options =>
            options.UseSqlServer(configItem.UserConnectionString));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Context 
            //builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer("constring"));

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
