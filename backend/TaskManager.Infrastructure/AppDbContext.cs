using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure;
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<Product> Products => Set<Product>();

    public DbSet<TestTable1> TestTable1s => Set<TestTable1>();
}
