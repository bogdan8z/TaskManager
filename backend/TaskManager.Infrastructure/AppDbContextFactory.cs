using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TaskManager.Infrastructure;
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
         var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        var connectionString = GetConnectionString(config);

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();       
        optionsBuilder.UseSqlServer(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }

    public static string GetConnectionString(IConfigurationRoot config)
    {
        // Attempt to get connection string from environment variable
        var envConnection = Environment.GetEnvironmentVariable("MYAPP_CONNECTION_STRING");
        if (!string.IsNullOrEmpty(envConnection))
        {
            return envConnection;
        }

        // Fallback to appsettings.json     
        var defaultConnection = config.GetConnectionString("Default");
        if (string.IsNullOrEmpty(defaultConnection))
        {
            throw new InvalidOperationException("Connection string not configured. Set ConnectionStrings:Default in appsettings or set MYAPP_CONNECTION_STRING environment variable.");
        }

        return defaultConnection;
    }
}