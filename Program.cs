
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using VagueVault.Backend.Configurations;
using VagueVault.Backend.Data;
using VagueVault.Backend.Middleware;


namespace VagueVault
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                
            })

            .AddJwtBearer(options =>
            {
             options.TokenValidationParameters = new TokenValidationParameters
             {
             ValidateIssuerSigningKey = true,
             IssuerSigningKey = new SymmetricSecurityKey(
             Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),
             ValidateIssuer = false,
             ValidateAudience = false,
             ValidateLifetime = true,
             ClockSkew = TimeSpan.Zero
              };
            });

            // Add authorization
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy =>
                            policy.RequireRole("Admin"));
            });



            builder.Services.AddSwaggerWithJwt();

            builder.Services.ConfigureMapper();
            builder.Services.ConfigureRepository();
            builder.Services.ConfigureHelpers(builder.Configuration);
            builder.Services.AddCloudinary(builder.Configuration);
            builder.Services.AddPayPal(builder.Configuration);
            builder.Services.ConfigureServices();
            builder.Services.ConfigureDbContext(builder.Configuration);




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

         


            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<VagueVaultDbContext>();
                db.Database.Migrate();
            }

            app.Run();
        }
    }
}
