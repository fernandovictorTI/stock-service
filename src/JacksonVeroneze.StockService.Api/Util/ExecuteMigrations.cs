using JacksonVeroneze.StockService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace JacksonVeroneze.StockService.Api.Util
{
    public static class ExecuteMigrations
    {
        public static void Execute(IHost host)
        {
            Log.Information("Migrations: {0}", "Performing migrations");

            using IServiceScope scope = host.Services.CreateScope();

            DatabaseContext databaseContext =
                scope.ServiceProvider.GetRequiredService<DatabaseContext>();

            databaseContext.Database.Migrate();
        }
    }
}
