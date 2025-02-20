
using ECommerce.Auth.Service.AuthDbContext;
using ECommerce.Auth.Service.Service;
using ECommerce.Auth.Service.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Auth.Service
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
            builder.Services.AddDbContext<AuthContext>(options =>
                options.UseSqlServer(configItem.UserConnectionString, builder =>
                {
                    builder.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
                    builder.CommandTimeout(30);
                }));

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

            // Service
            builder.Services.AddScoped<IAuthService, AuthService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("ECommerceCorsPolicy");

            app.UseCookiePolicy();

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
