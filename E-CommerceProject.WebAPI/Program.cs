
using E_CommerceProject.Business.Products.ModelValidator;
using E_CommerceProject.Business.Shared;
using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Infrastructure.Core.Base;
using E_CommerceProject.Infrastructure.Repositories.Brands;
using E_CommerceProject.Infrastructure.Shared;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace E_CommerceProject.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //var configuration = new ConfigurationBuilder()
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ECommerceContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
