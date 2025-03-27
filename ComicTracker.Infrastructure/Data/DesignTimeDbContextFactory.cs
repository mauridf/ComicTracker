using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ComicTracker.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ComicTrackerDbContext>
    {
        public ComicTrackerDbContext CreateDbContext(string[] args)
        {
            // Aponte para o appsettings.json no projeto API
            var path = Path.Combine(Directory.GetCurrentDirectory(), "../ComicTracker.API");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<ComicTrackerDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseNpgsql(connectionString);

            return new ComicTrackerDbContext(builder.Options);
        }
    }
}