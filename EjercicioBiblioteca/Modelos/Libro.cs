using System.ComponentModel.DataAnnotations;
namespace EjercicioBiblioteca.Modelos
{
    public class Libro
    {
        [Key]
        public int id_libro { get; set; }
        public string titulo { get; set; }
        public int? anioPublicacion { get; set; }
        public int? autor_id { get; set; }
        public int? categoria_id { get; set; }
        public string resumen { get; set; }
    }
}
