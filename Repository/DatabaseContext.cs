namespace Repository;
using Domain;
using Microsoft.EntityFrameworkCore;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {

    }

    public DbSet<Prompt> Prompts { get; set; }

    public DbSet<Versao> Versoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define primary key using Fluent API
        modelBuilder.Entity<Prompt>()
            .HasKey(p => p.id);

        modelBuilder.Entity<Versao>()
            .HasKey(v => v.idVersao);
    }

}