using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ClinicaSistema.Models
{
    public partial class dbclinicaContext : DbContext
    {
        public dbclinicaContext()
        {
        }

        public dbclinicaContext(DbContextOptions<dbclinicaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Consulta> Consultas { get; set; } = null!;
        public virtual DbSet<Especialidade> Especialidades { get; set; } = null!;
        public virtual DbSet<Medicamento> Medicamentos { get; set; } = null!;
        public virtual DbSet<Medico> Medicos { get; set; } = null!;
        public virtual DbSet<Paciente> Pacientes { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=srv-eno.database.windows.net;User Id=admin.eno;Password=erasmo.123;Database=db-clinica");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Consulta>(entity =>
            {
                entity.Property(e => e.ConsultaId).HasColumnName("ConsultaID");

                entity.Property(e => e.DataConsulta).HasColumnType("date");

                entity.Property(e => e.EspecialidadeId).HasColumnName("EspecialidadeID");

                entity.Property(e => e.MedicoId).HasColumnName("MedicoID");

                entity.Property(e => e.PacienteId).HasColumnName("PacienteID");

                entity.Property(e => e.Procedimento)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Especialidade)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.EspecialidadeId)
                    .HasConstraintName("FK__Consultas__Espec__6A30C649");

                entity.HasOne(d => d.Medico)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.MedicoId)
                    .HasConstraintName("FK__Consultas__Medic__6B24EA82");

                entity.HasOne(d => d.Paciente)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.PacienteId)
                    .HasConstraintName("FK__Consultas__Pacie__6C190EBB");
            });

            modelBuilder.Entity<Especialidade>(entity =>
            {
                entity.Property(e => e.EspecialidadeId).HasColumnName("EspecialidadeID");

                entity.Property(e => e.Nome)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Medicamento>(entity =>
            {
                entity.Property(e => e.MedicamentoId).HasColumnName("MedicamentoID");

                entity.Property(e => e.ConsultaId).HasColumnName("ConsultaID");

                entity.Property(e => e.Dosagem)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NomeMedicamento)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Consulta)
                    .WithMany(p => p.Medicamentos)
                    .HasForeignKey(d => d.ConsultaId)
                    .HasConstraintName("FK__Medicamen__Consu__6EF57B66");
            });

            modelBuilder.Entity<Medico>(entity =>
            {
                entity.HasKey(e => e.MedicosId)
                    .HasName("PK__Medicos__9FB1F6DBFFE8CA0F");

                entity.Property(e => e.MedicosId).HasColumnName("MedicosID");

                entity.Property(e => e.EspecialidadeId).HasColumnName("EspecialidadeID");

                entity.Property(e => e.Nome)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Especialidade)
                    .WithMany(p => p.Medicos)
                    .HasForeignKey(d => d.EspecialidadeId)
                    .HasConstraintName("FK__Medicos__Especia__5EBF139D");
            });

            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.Property(e => e.PacienteId).HasColumnName("PacienteID");

                entity.Property(e => e.Nome)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
