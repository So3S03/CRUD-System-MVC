using Karim.CRUD.DAL.Persistence.Data;
using Karim.CRUD.DAL.Persistence.Initializer;
using Microsoft.EntityFrameworkCore;

namespace Karim.CRUD.PL.Helpers
{
    public static class Initializer
    {
        public static async Task<WebApplication> InitializeDbAsync(this WebApplication app)
        {
            using var Scope = app.Services.CreateAsyncScope();
            var Services = Scope.ServiceProvider;
            var Initializer = Services.GetRequiredService<IDbInitializer>();
            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                await Initializer.InitializeAsync();
                //await Initializer.SeedAsync();
            }
            catch (Exception ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "An Error Occured During Apply The Migrations Or The Data Seeding");
                throw;
            }
            return app;
        }
    }
}
