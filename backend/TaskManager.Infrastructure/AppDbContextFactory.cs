using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TaskManager.Infrastructure;
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var connectionString = Environment.GetEnvironmentVariable("MYAPP_CONNECTION_STRING");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string not set in environment variables.");
        }

        optionsBuilder.UseSqlServer(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}