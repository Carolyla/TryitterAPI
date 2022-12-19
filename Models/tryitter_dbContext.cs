using Microsoft.EntityFrameworkCore;
using TryitterApi.Models;

namespace TryitterApi.Context
{
public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options)
            : base(options)
    { }

    // Adicionamos Student como um DbSet
    public DbSet<Student>? Students { get; set; }
    public DbSet<Post>? Posts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
    
}