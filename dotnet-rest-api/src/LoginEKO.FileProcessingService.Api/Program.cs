using LoginEKO.FileProcessingService.CompositionRoot.Extensions;
using LoginEKO.FileProcessingService.Persistence.Database;
using System.Threading.Tasks;

namespace LoginEKO.FileProcessingService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;
             
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDomain();
            builder.Services.AddDatabase(config["Database:ConnectionString"]!);

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

            //var dbInitializer = app.Services.GetRequiredService<DbInitializer>();
            //await dbInitializer.InitializeAsync();

            app.Run();
        }
    }
}
