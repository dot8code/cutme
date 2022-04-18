using CutMe.Models;
using Microsoft.EntityFrameworkCore;

namespace CutMe.Storage;

public class RedirectionDbContext : DbContext
{
    public DbSet<RedirectInformation> RedirectInformations { get; set; } 

    public RedirectionDbContext(DbContextOptions<RedirectionDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RedirectInformation>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
    }
}