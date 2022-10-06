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
    }
}
