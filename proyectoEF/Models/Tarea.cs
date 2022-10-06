using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectoEF.Models
{
    public class Tarea
    {
        //Especificando la clave principal de la tabla con la etiqueta Key
        //[Key]
        public Guid TareaId { get; set; }

        //Creando relacion con la Categoria
        //Especificando la llave foranea de la tabla con la etiqueta ForeignKey
        //[ForeignKey("categoriaId")]
        public Guid CategoriaId { get; set; }

        //Especificando los campos requeridos con la etiqueta Required
        //[Required]
        //Especificando la cantidad maxima de caracteres en el titulo
        //[MaxLength(200)]
        public string Titulo { get; set; }

        public string Descripcion { get; set; }
        
        public Prioridad PrioridadTarea { get; set; }

        public DateTime FechaCreacion { get; set; }

        //Creando relacion con modelo Categoria
        public virtual Categoria Categoria { get; set; }

        //La key NotMapped determina que este atributo no debe de ser guardado en la base de datos
        //[NotMapped]
        public string Resumen { get; set; }

    }

    public enum Prioridad
    {
        Baja,
        Media,
        Alta
    }
}
