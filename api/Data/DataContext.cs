using YonderfulApi.Models;
using Microsoft.EntityFrameworkCore;

namespace YonderfulApi.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<TaskEmp> Tasks { get; set; }
    public DbSet<User> Users { get; set; }
 
    public DbSet<TaskPresence> TaskPresence { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
            base.OnModelCreating(builder);

            builder.Entity<TaskPresence>().HasKey(i => new { i.TaskId, i.UserId });
    }
    
  }
}