using System.ComponentModel.DataAnnotations;
namespace EjercicioBiblioteca.Modelos
{
    public class Autor
    {
        [Key]
        public int id_Autor { get; set; }
        public string Nombre { get; set; }


    }
}
