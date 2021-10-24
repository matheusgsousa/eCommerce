using System;
using eCommerce.Models.Table;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace eCommerce.Models.DataContext
{
  public partial class eCommerceContext : DbContext
  {
    public eCommerceContext()
    {
    }

    public eCommerceContext(DbContextOptions<eCommerceContext> options)
        : base(options)
    {
    }
    public virtual DbSet<PessoaFisica> PessoaFisicas { get; set; }
    public virtual DbSet<Cliente> Clientes { get; set; }
    public virtual DbSet<PessoaJuridica> PessoaJuridicas { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_100_CI_AI");

      modelBuilder.Entity<Cidade>(entity =>
      {
        entity.HasKey(e => e.IdCidade)
                  .HasName("PK__Cidade__160879A35C50D101");

        entity.ToTable("Cidade");

        entity.Property(e => e.IdUf).HasColumnName("IdUF");

        entity.Property(e => e.Nome)
                  .IsRequired()
                  .HasMaxLength(200)
                  .IsUnicode(false);

        entity.HasOne(d => d.IdUfNavigation)
                  .WithMany(p => p.Cidades)
                  .HasForeignKey(d => d.IdUf)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_UF_Cidade");
      });

      modelBuilder.Entity<Cliente>(entity =>
      {
        entity.HasKey(e => e.IdPessoa)
                  .HasName("PK__Cliente__7061465D4B0B807C");

        entity.ToTable("Cliente");

        entity.Property(e => e.IdPessoa).ValueGeneratedNever();

        entity.HasOne(d => d.IdPessoaNavigation)
                  .WithOne(p => p.Cliente)
                  .HasForeignKey<Cliente>(d => d.IdPessoa)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_Pessoa_Cliente");
      });
      modelBuilder.Entity<PessoaFisica>(entity =>
    {
      entity.HasKey(e => e.IdPessoa)
                  .HasName("PK__PessoaFi__7061465D0AA9C6A4");

      entity.ToTable("PessoaFisica");

      entity.Property(e => e.IdPessoa).ValueGeneratedNever();

      entity.Property(e => e.DataNascimento).HasColumnType("date");

      entity.Property(e => e.Rg)
                  .IsRequired()
                  .HasMaxLength(15)
                  .IsUnicode(false)
                  .HasColumnName("RG");

      entity.HasOne(d => d.IdPessoaNavigation)
                  .WithOne(p => p.PessoaFisica)
                  .HasForeignKey<PessoaFisica>(d => d.IdPessoa)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_Pessoa_PessoaFisica");
    });

      modelBuilder.Entity<PessoaJuridica>(entity =>
      {
        entity.HasKey(e => e.IdPessoa)
                  .HasName("PK__PessoaJu__7061465D232A187D");

        entity.ToTable("PessoaJuridica");

        entity.Property(e => e.IdPessoa).ValueGeneratedNever();

        entity.Property(e => e.InscricaoEstadual)
                  .IsRequired()
                  .HasMaxLength(20)
                  .IsUnicode(false);

        entity.Property(e => e.NomeFantasia)
                  .IsRequired()
                  .HasMaxLength(200)
                  .IsUnicode(false);

        entity.HasOne(d => d.IdPessoaNavigation)
                  .WithOne(p => p.PessoaJuridica)
                  .HasForeignKey<PessoaJuridica>(d => d.IdPessoa)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_Pessoa_PessoaJuridica");
      });
      OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}


