
using Azure.Core;
using E_CommerceProject.Business.Emails;
using E_CommerceProject.Business.Emails.Dtos;
using E_CommerceProject.Business.Emails.Interfcaes;
using E_CommerceProject.Business.Products.ModelValidator;
using E_CommerceProject.Business.Shared;
using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Infrastructure.Repositories;
using E_CommerceProject.Infrastructure.Repositories.cart;
using E_CommerceProject.Infrastructure.Shared;
using E_CommerceProject.Models.Models;
using E_CommerceProject.WebAPI.Helper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace E_CommerceProject.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllDomains", p =>
                {
                    p.AllowAnyOrigin();
                    p.AllowAnyHeader();
                    p.AllowAnyMethod();
                });
            });

            builder.Host.UseSerilog();
            // automapper
            builder.Services.AddAutoMapper(config =>
            {
                config.AddMaps("E-CommerceProject.Business");
                config.AddMaps("E-CommerceProject.WebAPI");
            });

            // register all validators
            builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();


            // register services to the container.
            builder.Services.AddRepositories();
            builder.Services.AddServices();
            builder.Services.AddScoped<IFileProvider, FileProvider>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            ////////////////////////////////////////////////////////
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().
                AddEntityFrameworkStores<ECommerceContext>();


            // builder.Services.AddScoped<IUserRepository, UserRepository>();




            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false, // Set to true if you have an issuer
                    ValidateAudience = false, // Set to true if you have an audience
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("your_secret_key_here"))
                };
            });



            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
            });

            // Other configurations...
            builder.Services.AddScoped<ICartRepository,CartRepository>();

          



            //forgetpassword
            builder.Services.Configure<IdentityOptions>(
                opt => opt.SignIn.RequireConfirmedEmail = true
            );

            builder.Services.Configure<DataProtectionTokenProviderOptions>(
                opt => opt.TokenLifespan = TimeSpan.FromHours(10)
                );

            //add email configuration
            var emailConfig = configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();

            builder.Services.AddSingleton(emailConfig);
            builder.Services.AddScoped<IEmailService, EmailService>();

            // For Authentication
            builder.Services.AddAuthentication(options =>
 {
     // This specifies that JWT Bearer authentication will be used as the default
     // authentication scheme for authenticating and challenging requests./
     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

 }).AddJwtBearer(options =>
 {
     // instructs the middleware to save the token in the
     // authentication properties after a successful authentication.
     options.SaveToken = true;
     //allows the use of HTTP (non-HTTPS) requests for token validation. 
     options.RequireHttpsMetadata = false;
     options.TokenValidationParameters = new TokenValidationParameters()
     {
         ValidateIssuer = false,
         ValidateAudience = false,
         ValidAudience = configuration["JWT:ValidAudience"],
         ValidIssuer = configuration["JWT:ValidIssuer"],
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
     };
 });


            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ECommerceContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Auth API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {{
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                    }
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAllDomains");

            app.UseAuthentication();
            app.UseAuthorization();
      


            app.MapControllers();

            app.Run();
        }
    }
}
