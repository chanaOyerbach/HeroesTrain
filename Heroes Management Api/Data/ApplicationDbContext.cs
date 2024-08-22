using Heroes_Management_Api.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Hero> Heroes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Trainer>()
            .HasMany(t => t.Heroes)
            .WithOne(h => h.Trainer)
            .HasForeignKey(h => h.TrainerId);

        base.OnModelCreating(modelBuilder);
    }
}