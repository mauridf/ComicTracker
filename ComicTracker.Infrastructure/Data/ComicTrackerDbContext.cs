namespace ComicTracker.Infrastructure.Data;
using ComicTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class ComicTrackerDbContext : DbContext
{
    public ComicTrackerDbContext(DbContextOptions<ComicTrackerDbContext> options)
        : base(options) { }

    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Volume> Volumes { get; set; }
    public DbSet<Issue> Issues { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurações de índices únicos para IDs da ComicVine
        modelBuilder.Entity<Publisher>()
            .HasIndex(p => p.ComicVineId)
            .IsUnique();

        modelBuilder.Entity<Character>()
            .HasIndex(c => c.ComicVineId)
            .IsUnique();

        modelBuilder.Entity<Team>()
            .HasIndex(t => t.ComicVineId)
            .IsUnique();

        modelBuilder.Entity<Volume>()
            .HasIndex(v => v.ComicVineId)
            .IsUnique();

        modelBuilder.Entity<Issue>()
            .HasIndex(i => i.ComicVineId)
            .IsUnique();

        // Configurações de relacionamentos
        modelBuilder.Entity<Volume>()
            .HasMany(v => v.Issues)
            .WithOne(i => i.Volume)
            .HasForeignKey(i => i.VolumeId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Publisher>()
            .HasMany(p => p.Volumes)
            .WithOne(v => v.Publisher)
            .HasForeignKey(v => v.PublisherId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configurações de tamanho máximo para campos string
        modelBuilder.Entity<Publisher>()
            .Property(p => p.Name)
            .HasMaxLength(200);

        modelBuilder.Entity<Character>()
            .Property(c => c.Name)
            .HasMaxLength(200);

        // Configuração para campos opcionais
        modelBuilder.Entity<Issue>()
            .Property(i => i.Notes)
            .IsRequired(false);
    }
}