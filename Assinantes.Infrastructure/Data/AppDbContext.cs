using Assinantes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assinantes.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Assinante> Assinantes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assinante>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.NomeCompleto).IsRequired().HasMaxLength(200);
            entity.Property(a => a.Email).IsRequired().HasMaxLength(150);
            entity.HasIndex(a => a.Email).IsUnique();
            entity.Property(a => a.DataInicioAssinatura).IsRequired();
            entity.Property(a => a.Plano).IsRequired();
            entity.Property(a => a.ValorMensal).IsRequired().HasColumnType("decimal(10,2)");
            entity.Property(a => a.Ativo).IsRequired();
            entity.Ignore(a => a.TempoAssinaturaEmMeses);
        });
    }
}
