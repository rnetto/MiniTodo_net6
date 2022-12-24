using Microsoft.EntityFrameworkCore;

public class Afazer { public int Id { get; set; } public string? Titulo { get; set; }
                    public bool Realizado { get; set; } = false; public DateTime DataCadastro { get; set; } = DateTime.Now;
};
public class CriarAfazer { public string? Titulo { get; set; } };
public class EditarAfazer { public string? Titulo { get; set; } public bool? Realizado { get; set; } };

public class AppDbContext : DbContext
{
    public AppDbContext() { }
    public DbSet<Afazer> Afazers { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite(connectionString: "DataSource= miniApp.db; Cache=Shared");
}