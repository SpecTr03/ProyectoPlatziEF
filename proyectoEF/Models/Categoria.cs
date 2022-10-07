using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace proyectoEF.Models
{
    public class Categoria
    {
        //Especificando la clave principal de la tabla con la etiqueta Key
        //[Key]
        public Guid CategoriaId { get; set; }

        //Especificando los campos requeridos con la etiqueta Required
        //[Required]
        //Especificando la cantidad maxima de caracteres en el nombre
        //[MaxLength(150)]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public int Peso { get; set; }

        //Creando relacion con Tarea
        [JsonIgnore]
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}
