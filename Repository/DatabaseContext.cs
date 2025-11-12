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

}