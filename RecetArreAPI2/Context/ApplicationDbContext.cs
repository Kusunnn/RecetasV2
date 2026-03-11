using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecetArreAPI2.Models;

namespace RecetArreAPI2.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<Receta> Recetas { get; set; }
        public DbSet<Tiempo> Tiempos { get; set; }
        public DbSet<Rec_Tiem> RecetasTiempos { get; set; }
        public DbSet<Rec_Cat> RecetasCategorias { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configuración de Categoria
            builder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsRequired(false);

                entity.Property(e => e.CreadoUtc)
                    .IsRequired()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                // Relación con ApplicationUser
                entity.HasOne(e => e.CreadoPorUsuario)
                    .WithMany()
                    .HasForeignKey(e => e.CreadoPorUsuarioId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired(false);

                // Índices
                entity.HasIndex(e => e.Nombre).IsUnique();
                entity.HasIndex(e => e.CreadoPorUsuarioId);
            });

            // Configuración de Ingrediente
            builder.Entity<Ingrediente>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UnidadMed)
                    .HasMaxLength(20)
                    .IsRequired(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsRequired(false);

                // Índice único para evitar duplicados por nombre
                entity.HasIndex(e => e.Nombre).IsUnique();
            });

            // Configuración de Recetas
            builder.Entity<Receta>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Instrucciones)
                    .HasMaxLength(3000)
                    .IsRequired(false);

                entity.Property(e => e.CreadoUtc)
                    .IsRequired()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(e => e.Usuario)
                    .WithMany()
                    .HasForeignKey(e => e.IdUsuario)
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired(false);

                entity.HasIndex(e => e.IdUsuario);
            });

            // Configuración de Tiempo
            builder.Entity<Tiempo>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsRequired(false);
            });

            // Tabla intermedia Recetas <-> Tiempo (muchos a muchos)
            builder.Entity<Rec_Tiem>(entity =>
            {
                entity.HasKey(e => new { e.IdReceta, e.IdTiempo });

                entity.HasOne(e => e.Receta)
                    .WithMany(r => r.IdTiempo)
                    .HasForeignKey(e => e.IdReceta)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Tiempo)
                    .WithMany(t => t.RecetasTiempos)
                    .HasForeignKey(e => e.IdTiempo)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.ToTable("Rec_Tiem");
            });

            // Tabla intermedia Recetas <-> Categorias (muchos a muchos)
            builder.Entity<Rec_Cat>(entity =>
            {
                entity.HasKey(e => new { e.IdReceta, e.IdCategoria });

                entity.HasOne(e => e.Receta)
                    .WithMany(r => r.RecetasCategorias)
                    .HasForeignKey(e => e.IdReceta)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Categoria)
                    .WithMany(c => c.RecetasCategorias)
                    .HasForeignKey(e => e.IdCategoria)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.ToTable("Rec_Cat");
            });
            // Configuración de Comentarios
            builder.Entity<Comentario>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.TextoCom)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Puntuacion)
                    .IsRequired();

                entity.Property(e => e.Fecha)
                    .IsRequired()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(e => e.Usuario)
                    .WithMany()
                    .HasForeignKey(e => e.IdUsuario)
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired(false);

                entity.HasOne(e => e.Receta)
                    .WithMany(r => r.Comentarios)
                    .HasForeignKey(e => e.IdReceta)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                entity.HasIndex(e => e.IdUsuario);
                entity.HasIndex(e => e.IdReceta);
            });
        }
    }
}
