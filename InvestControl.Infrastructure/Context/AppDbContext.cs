using InvestControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestControl.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Ativo> Ativos => Set<Ativo>();
    public DbSet<Operacao> Operacoes => Set<Operacao>();
    public DbSet<Cotacao> Cotacoes => Set<Cotacao>();
    public DbSet<Posicao> Posicoes => Set<Posicao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Email)
            .IsUnique();
        modelBuilder.Entity<Ativo>()
            .HasIndex(a => a.Codigo)
            .IsUnique();
        modelBuilder.Entity<Operacao>()
            .Property(o => o.TipoOperacao)
            .HasMaxLength(10);

        base.OnModelCreating(modelBuilder);
    }
}
