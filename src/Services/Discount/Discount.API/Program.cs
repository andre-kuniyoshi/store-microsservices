using Discount.API.Extensions;
using Discount.Infra.Extensions;
using Discount.Application.Extensions;
using Core.Configurations;
using Serilog;

namespace Discount.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            HostConfigurations.ConfigureSerilog();
            builder.Host.UseSerilog();

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddApplicationLayer();
            builder.Services.AddInfraLayer();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.MigrateDatabase<Program>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}