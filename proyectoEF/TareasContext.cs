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
            //Agregando datos semilla a las tablas de Categoria en la DB (datos inciales)
            List<Categoria> categoriasInit = new List<Categoria>();
            //Recordar usar un Guid generator para que no se cree nuevas claves
            categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("579ca17b-a05b-41e2-94fe-e90db5147889"), Nombre = "Actividades pendientes", Peso = 20 });
            categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("579ca17b-a05b-41e2-94fe-e90db5147823"), Nombre = "Actividades personales", Peso = 50 });

            modelBuilder.Entity<Categoria>(categoria =>
            {
                categoria.ToTable("Categoria");
                categoria.HasKey(p => p.CategoriaId);

                categoria.Property(p => p.Nombre).IsRequired().HasMaxLength(150);
                categoria.Property(p => p.Descripcion).IsRequired(false);
                categoria.Property(p => p.Peso);

                categoria.HasData(categoriasInit);
            });

            //Agregando datos semilla a las tablas de Tarea en la DB (datos inciales)
            List<Tarea> tareasInit = new List<Tarea>();

            tareasInit.Add(new Tarea()
            {
                TareaId = Guid.Parse("579ca17b-a05b-41e2-94fe-e90db5147810"),
                CategoriaId = Guid.Parse("579ca17b-a05b-41e2-94fe-e90db5147889"),
                PrioridadTarea = Prioridad.Media,
                Titulo = "Pago de servicios publicos",
                FechaCreacion = DateTime.Now
            });

            tareasInit.Add(new Tarea()
            {
                TareaId = Guid.Parse("579ca17b-a05b-41e2-94fe-e90db5147811"),
                CategoriaId = Guid.Parse("579ca17b-a05b-41e2-94fe-e90db5147823"),
                PrioridadTarea = Prioridad.Baja,
                Titulo = "Terminar de ver pelicula en netflix",
                FechaCreacion = DateTime.Now
            });

            modelBuilder.Entity<Tarea>(tarea =>
            {
                tarea.ToTable("Tarea");
                tarea.HasKey(p => p.TareaId);

                tarea.HasOne(p => p.Categoria).WithMany(p => p.Tareas).HasForeignKey(p => p.CategoriaId);

                tarea.Property(p => p.Titulo).IsRequired().HasMaxLength(200);
                tarea.Property(p => p.Descripcion).IsRequired(false);
                tarea.Property(p => p.PrioridadTarea);
                tarea.Property(p => p.FechaCreacion).HasDefaultValue(DateTime.Now); ;

                tarea.Ignore(p => p.Resumen);

                tarea.HasData(tareasInit);
            });
        }
    }
}
