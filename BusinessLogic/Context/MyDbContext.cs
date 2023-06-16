using System;
using System.Collections.Generic;
using BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Evento> Eventos { get; set; }

    public virtual DbSet<Inscricao> Inscricoes { get; set; }

    public virtual DbSet<Participante> Participantes { get; set; }

    public virtual DbSet<TipoUtilizador> Tipoutilizadors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=es2;Username=es2;Password=es2;SearchPath=public;Port=15432");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresExtension("postgis")
            .HasPostgresExtension("uuid-ossp")
            .HasPostgresExtension("topology", "postgis_topology");

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("eventos_pkey");

            entity.ToTable("eventos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataFim).HasColumnName("data_fim");
            entity.Property(e => e.DataInicio).HasColumnName("data_inicio");
            entity.Property(e => e.Local)
                .HasMaxLength(100)
                .HasColumnName("local");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Inscricao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("inscricoes_pkey");

            entity.ToTable("inscricoes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataInscricao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data_inscricao");
            entity.Property(e => e.EventoId).HasColumnName("evento_id");
            entity.Property(e => e.ParticipanteId).HasColumnName("participante_id");

            entity.HasOne(d => d.Evento).WithMany(p => p.Inscricos)
                .HasForeignKey(d => d.EventoId)
                .HasConstraintName("inscricoes_evento_id_fkey");

            entity.HasOne(d => d.Participante).WithMany(p => p.Inscricos)
                .HasForeignKey(d => d.ParticipanteId)
                .HasConstraintName("inscricoes_participante_id_fkey");
        });

        modelBuilder.Entity<Participante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("participantes_pkey");

            entity.ToTable("participantes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
            entity.Property(e => e.Telefone)
                .HasMaxLength(20)
                .HasColumnName("telefone");
            entity.Property(e => e.Tipoutilizador).HasColumnName("tipoutilizador");
        });

        modelBuilder.Entity<TipoUtilizador>(entity =>
        {
            entity.HasKey(e => e.TipoId).HasName("tipoutilizador_pkey");

            entity.ToTable("tipoutilizador");

            entity.Property(e => e.TipoId).HasColumnName("tipo_id");
            entity.Property(e => e.ParticipanteId).HasColumnName("participante_id");
            entity.Property(e => e.Tipo)
                .HasMaxLength(100)
                .HasColumnName("tipo");

            entity.HasOne(d => d.Participante).WithMany(p => p.Tipoutilizadors)
                .HasForeignKey(d => d.ParticipanteId)
                .HasConstraintName("tipoutilizador_participante_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
