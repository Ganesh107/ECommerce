
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

            // Configure authentication scheme
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configItem.Issuer,
                    ValidAudience = configItem.Audience,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configItem.JwtKey!))
                };
            });

            // Context
            builder.Services.AddDbContext<AuthContext>(options =>
                options.UseSqlServer(configItem.UserConnectionString));

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
            app.UseCookiePolicy();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
