using YonderfulApi.Models;
using Microsoft.EntityFrameworkCore;

namespace YonderfulApi.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Picture> Pictures { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Location> Location { get; set; }
    public DbSet<Attendance> Attendance { get; set; }
    public DbSet<TaskPresence> TaskPresence { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
            base.OnModelCreating(builder);

            builder.Entity<Attendance>().HasKey(i => new { i.EventId, i.UserId });
            builder.Entity<TaskPresence>().HasKey(i => new { i.CategoryId, i.UserId });
    }
    
  }
}