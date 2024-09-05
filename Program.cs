
using gtm.Db;
using Microsoft.EntityFrameworkCore;

namespace gtm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Entity framework
            builder.Services.AddDbContext<GtmContext>();

            // Logging
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

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

            // Migrate database on startup
            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    app.Logger.LogInformation("Running database migrations");

                    var dbContext = scope.ServiceProvider.GetRequiredService<GtmContext>();
                    await dbContext.Database.MigrateAsync();

                    app.Logger.LogInformation("Completed database migrations");
                } catch (Exception ex)
                {
                    app.Logger.LogError("Database migrations failed");
                    app.Logger.LogError($"{ex.Message}");
                }
            }

            app.Run();
        }
    }
}
