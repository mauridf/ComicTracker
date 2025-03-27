namespace ComicTracker.Infrastructure.Data;
using ComicTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

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
        // Configurações de relacionamento e índices
        modelBuilder.Entity<Volume>()
            .HasIndex(v => v.ComicVineId)
            .IsUnique();

        modelBuilder.Entity<Issue>()
            .HasIndex(i => i.ComicVineId)
            .IsUnique();

        // Outras configurações...
    }
}