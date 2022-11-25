using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcStorage;

public class EfcContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcStorage/RedditClone.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(user => user.Username);
        modelBuilder.Entity<Post>().HasKey(post => post.Id);
    }
}