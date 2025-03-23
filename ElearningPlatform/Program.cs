using Core.Entities;
using DotNetEnv;
using Infrastructure.DB;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace ZeroProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Env.Load();

            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Explicitly add logging (optional, as it's included by default in WebApplication.CreateBuilder)
            builder.Services.AddLogging(logging => logging.AddConsole());

            // Configure Database
            builder.Services.AddDbContext<ElearningDbContext>(options =>
                options.UseSqlServer(Environment.GetEnvironmentVariable("ELEARNINGPLATFORM_DEV_DATABASE")));

            // Configure Identity
            //builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            //{
            //    options.Password.RequiredLength = 5;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequireLowercase = true;
            //    options.Password.RequireDigit = true;
            //})
            //.AddEntityFrameworkStores<ApplicationContext>()
            //.AddDefaultTokenProviders()
            //.AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationContext, Guid>>()
            //.AddRoleStore<RoleStore<ApplicationRole, ApplicationContext, Guid>>();

            //// Configure JWT Authentication
            //var jwtSecretKey = configuration["Jwt:SecretKey"];
            //if (string.IsNullOrEmpty(jwtSecretKey))
            //{
            //    throw new InvalidOperationException("JWT Secret Key is missing in the configuration.");
            //}

            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //    options.RequireHttpsMetadata = false;
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidIssuer = configuration["Jwt:Issuer"],
            //        ValidateAudience = false,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey))
            //    };

            //    options.Events = new JwtBearerEvents
            //    {
            //        OnAuthenticationFailed = context =>
            //        {
            //            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            //            logger.LogError($"Authentication failed: {context.Exception.Message}");
            //            return Task.CompletedTask;
            //        },
            //        OnTokenValidated = context =>
            //        {
            //            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            //            logger.LogInformation("Token validated successfully");
            //            return Task.CompletedTask;
            //        }
            //    };
            //});

            // Authorization & Policies
            builder.Services.AddAuthorization();
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add(new ProducesAttribute("application/json"));
                options.Filters.Add(new ConsumesAttribute("application/json"));
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddXmlSerializerFormatters();

            // Enable Swagger (API Documentation)
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            // CORS Policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });

            var app = builder.Build();

            // Middleware Configuration
            if (app.Environment.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");

            //// Seed data
            //using (var scope = app.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    var logger = services.GetRequiredService<ILogger<Program>>(); // Define logger here
            //    try
            //    {
            //        await SeedData.Initialize(services);
            //    }
            //    catch (Exception ex)
            //    {
            //        logger.LogError(ex, "An error occurred while seeding the database.");
            //    }
            //}

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            //// Ensure Roles Exist Before Running App
            //using (var scope = app.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    await EnsureRolesCreated(services);
            //}

            app.Run();
        }

        //private static async Task EnsureRolesCreated(IServiceProvider services)
        //{
        //    var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();
        //    string[] roles = { "Admin", "User" };

        //    foreach (var role in roles)
        //    {
        //        if (!await roleManager.RoleExistsAsync(role))
        //        {
        //            await roleManager.CreateAsync(new ApplicationRole { Name = role });
        //        }
        //    }
        //}
    }
}