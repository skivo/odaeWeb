using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace odaeWeb.Models.DB
{
    public partial class odaeDBContext : DbContext
    {
        public virtual DbSet<Codificacion> Codificacion { get; set; }
        public virtual DbSet<Codificador> Codificador { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<Eje> Eje { get; set; }
        public virtual DbSet<Escuela> Escuela { get; set; }
        public virtual DbSet<Estudiante> Estudiante { get; set; }
        public virtual DbSet<Fase> Fase { get; set; }
        public virtual DbSet<Habilidad> Habilidad { get; set; }
        public virtual DbSet<Material> Material { get; set; }
        public virtual DbSet<Nivel> Nivel { get; set; }
        public virtual DbSet<Objetivo> Objetivo { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<TipoMaterial> TipoMaterial { get; set; }
        public virtual DbSet<TipoTarea> TipoTarea { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserFaseCodificador> UserFaseCodificador { get; set; }

        public odaeDBContext(DbContextOptions<odaeDBContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Codificacion>(entity =>
            {
                entity.HasKey(e => new { e.CodificadorId, e.FaseId, e.MaterialId });

                entity.Property(e => e.CodificadorId).HasColumnName("CodificadorID");

                entity.Property(e => e.FaseId).HasColumnName("FaseID");

                entity.Property(e => e.MaterialId).HasColumnName("MaterialID");

                entity.Property(e => e.CursoId).HasColumnName("CursoID");

                entity.Property(e => e.EjeId).HasColumnName("EjeID");

                entity.Property(e => e.HabilidadComentario).HasMaxLength(255);

                entity.Property(e => e.HabilidadId).HasColumnName("HabilidadID");

                entity.Property(e => e.NivelComentario).HasMaxLength(255);

                entity.Property(e => e.NivelId).HasColumnName("NivelID");

                entity.Property(e => e.ObjetivoComentario).HasMaxLength(255);

                entity.Property(e => e.ObjetivoId).HasColumnName("ObjetivoID");

                entity.Property(e => e.Observaciones).HasMaxLength(2000);

                entity.Property(e => e.TipoTareaComentario).HasMaxLength(255);

                entity.Property(e => e.TipoTareaId).HasColumnName("TipoTareaID");

                entity.Property(e => e.Estado).HasColumnName("Estado").HasDefaultValueSql("((0))");

                entity.Property(e => e.RowIndex).HasColumnName("RowIndex");

                entity.HasOne(d => d.Codificador)
                    .WithMany(p => p.Codificacion)
                    .HasForeignKey(d => d.CodificadorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Codificacion_Codificador");

                entity.HasOne(d => d.Fase)
                    .WithMany(p => p.Codificacion)
                    .HasForeignKey(d => d.FaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Codificacion_Fase");

                entity.HasOne(d => d.Habilidad)
                    .WithMany(p => p.Codificacion)
                    .HasForeignKey(d => d.HabilidadId)
                    .HasConstraintName("FK_Codificacion_Habilidad");

                entity.HasOne(d => d.Material)
                    .WithMany(p => p.Codificacion)
                    .HasForeignKey(d => d.MaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Codificacion_Material");

                entity.HasOne(d => d.Nivel)
                    .WithMany(p => p.Codificacion)
                    .HasForeignKey(d => d.NivelId)
                    .HasConstraintName("FK_Codificacion_Nivel");

                entity.HasOne(d => d.TipoTarea)
                    .WithMany(p => p.Codificacion)
                    .HasForeignKey(d => d.TipoTareaId)
                    .HasConstraintName("FK_Codificacion_TipoTarea");

                entity.HasOne(d => d.Objetivo)
                    .WithMany(p => p.Codificacion)
                    .HasForeignKey(d => new { d.ObjetivoId, d.CursoId, d.EjeId })
                    .HasConstraintName("FK_Codificacion_Objetivo");
            });

            modelBuilder.Entity<Codificador>(entity =>
            {
                entity.Property(e => e.CodificadorId)
                    .HasColumnName("CodificadorID")
                    .ValueGeneratedNever();

                entity.Property(e => e.NombreCodificador).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(255);

            });

            modelBuilder.Entity<Curso>(entity =>
            {
                entity.Property(e => e.CursoId)
                    .HasColumnName("CursoID")
                    .ValueGeneratedNever();

                entity.Property(e => e.NombreCurso)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Eje>(entity =>
            {
                entity.Property(e => e.EjeId)
                    .HasColumnName("EjeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DescripcionEje).HasMaxLength(50);
            });

            modelBuilder.Entity<Escuela>(entity =>
            {
                entity.HasIndex(e => e.RegionId)
                    .HasName("Escuela$EscuelaRegionID");

                entity.Property(e => e.EscuelaId)
                    .HasColumnName("EscuelaID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.NombreEscuela)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Escuela)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Escuela_Region");
            });

            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.HasIndex(e => e.EscuelaId)
                    .HasName("Estudiante$EstudianteEscuelaID");

                entity.Property(e => e.EstudianteId)
                    .HasColumnName("EstudianteID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Curso).HasDefaultValueSql("((0))");

                entity.Property(e => e.EscuelaId).HasColumnName("EscuelaID");

                entity.Property(e => e.NombreEstudiante).HasMaxLength(255);

                entity.HasOne(d => d.Escuela)
                    .WithMany(p => p.Estudiante)
                    .HasForeignKey(d => d.EscuelaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Estudiante_Escuela");
            });

            modelBuilder.Entity<Fase>(entity =>
            {
                entity.Property(e => e.FaseId)
                    .HasColumnName("FaseID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DescripcionFase).HasMaxLength(255);

                entity.Property(e => e.NombreFase)
                    .IsRequired()
                    .HasColumnType("nchar(10)");
            });

            modelBuilder.Entity<Habilidad>(entity =>
            {
                entity.Property(e => e.HabilidadId)
                    .HasColumnName("HabilidadID")
                    .ValueGeneratedNever();

                entity.Property(e => e.NombreHabilidad)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasIndex(e => e.EstudianteId)
                    .HasName("Material$EstudianteID");

                entity.HasIndex(e => e.Original)
                    .HasName("Material$MaterialOriginal");

                entity.HasIndex(e => e.TipoMaterialId)
                    .HasName("Material$TipoMaterialID");

                entity.Property(e => e.MaterialId)
                    .HasColumnName("MaterialID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.EstudianteId).HasColumnName("EstudianteID");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Original).HasDefaultValueSql("((0))");

                entity.Property(e => e.TipoMaterialId)
                    .IsRequired()
                    .HasColumnName("TipoMaterialID")
                    .HasMaxLength(1);

                entity.HasOne(d => d.Estudiante)
                    .WithMany(p => p.Material)
                    .HasForeignKey(d => d.EstudianteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Material_Estudiante");

                entity.HasOne(d => d.OriginalNavigation)
                    .WithMany(p => p.InverseOriginalNavigation)
                    .HasForeignKey(d => d.Original)
                    .HasConstraintName("FK_Material_Material");

                entity.HasOne(d => d.TipoMaterial)
                    .WithMany(p => p.Material)
                    .HasForeignKey(d => d.TipoMaterialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Material_TipoMaterial");
            });

            modelBuilder.Entity<Nivel>(entity =>
            {
                entity.Property(e => e.NivelId)
                    .HasColumnName("NivelID")
                    .ValueGeneratedNever();

                entity.Property(e => e.NivelDemandaCognitiva).HasMaxLength(50);
            });

            modelBuilder.Entity<Objetivo>(entity =>
            {
                entity.HasKey(e => new { e.ObjetivoId, e.CursoId, e.EjeId });

                entity.Property(e => e.ObjetivoId).HasColumnName("ObjetivoID");

                entity.Property(e => e.CursoId).HasColumnName("CursoID");

                entity.Property(e => e.EjeId).HasColumnName("EjeID");

                entity.Property(e => e.ObjetivoAprendizaje)
                    .IsRequired()
                    .HasMaxLength(4000);

                entity.HasOne(d => d.Curso)
                    .WithMany(p => p.Objetivo)
                    .HasForeignKey(d => d.CursoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Objetivo_Curso");

                entity.HasOne(d => d.Eje)
                    .WithMany(p => p.Objetivo)
                    .HasForeignKey(d => d.EjeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Objetivo_Eje");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.Property(e => e.ProfileId)
                    .HasColumnName("ProfileID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ProfileName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.Property(e => e.RegionId)
                    .HasColumnName("RegionID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.NombreRegion)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<TipoMaterial>(entity =>
            {
                entity.Property(e => e.TipoMaterialId)
                    .HasColumnName("TipoMaterialID")
                    .HasMaxLength(1)
                    .ValueGeneratedNever();

                entity.Property(e => e.TipoMaterial1)
                    .HasColumnName("TipoMaterial")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<TipoTarea>(entity =>
            {
                entity.Property(e => e.TipoTareaId)
                    .HasColumnName("TipoTareaID")
                    .ValueGeneratedNever();

                entity.Property(e => e.NombreTipoTarea)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasColumnType("char(5)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Password).HasColumnType("binary(64)");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.Token).HasColumnName("Token");

                entity.Property(e => e.TokenExpiration).HasColumnType("datetime");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Profile");
            });

            modelBuilder.Entity<UserFaseCodificador>(entity =>
            {
                entity.HasKey(e => new { e.UserId });

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasColumnType("char(5)");

                entity.Property(e => e.CodificadorId).HasColumnName("CodificadorID");

                entity.Property(e => e.FaseId).HasColumnName("FaseID");

                entity.HasOne(d => d.Codificador)
                    .WithMany(p => p.UserFaseCodificador)
                    .HasForeignKey(d => d.CodificadorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserFaseCodificador_Codificador");

                entity.HasOne(d => d.Fase)
                    .WithMany(p => p.UserFaseCodificador)
                    .HasForeignKey(d => d.FaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserFaseCodificador_Fase");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserFaseCodificador)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserFaseCodificador_User");
            });
        }
    }
}
