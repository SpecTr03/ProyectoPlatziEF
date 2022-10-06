using Microsoft.EntityFrameworkCore;
using proyectoEF.Models;

// Aqui se hacen las configuraciones del entity framework

/*
 * DBSet: Es un set o una asignación de datos del modelo que nosotros hemos creado previamente, básicamente esto va a representar lo que sería una tabla 
 * dentro del contexto de entity framework.
 * Contexto: Es donde van a ir todas las relaciones de los modelos que nosotros tenemos para poder transformarlo en colecciones 
 * que van a representarse dentro de la base de datos.
 */

namespace proyectoEF
{
    public class TareasContext : DbContext
    {
        public DbSet<Categoria> categorias { get; set; }
        public DbSet<Tarea> tareas { get; set; }

        public TareasContext(DbContextOptions<TareasContext> options) : base(options) { }

        //Aqui hacemos uso de FluentApi
        /*
         * FluenApi es una forma avanzada En la que se puede configurar los modelos, nos permite configurar las tablas y los atributos, agregando validaciones y 
         * restricciones.
         */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(categoria =>
            {
                categoria.ToTable("Categoria");
                categoria.HasKey(p => p.CategoriaId);

                categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);

                categoria.Property(p => p.Descripcion);
            });

            modelBuilder.Entity<Tarea>(tarea =>
            {
                tarea.ToTable("Tarea");
                tarea.HasKey(p => p.TareaId);

                tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);

                tarea.Property(p => p.Titulo).IsRequired().HasMaxLength(200);
                tarea.Property(p => p.Descripcion);
                tarea.Property(p => p.PrioridadTarea);
                tarea.Property(p => p.FechaCreacion);
                tarea.Ignore(p => p.Resumen);
            });
        }
    }
}
