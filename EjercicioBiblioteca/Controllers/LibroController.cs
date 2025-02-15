using EjercicioBiblioteca.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EjercicioBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly BibliotecaContext _BibliotecaContext;

        public LibroController(BibliotecaContext BibliotecaContext)
        {
            _BibliotecaContext = BibliotecaContext;
        }

        // Para poder ver todos los registros:

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Index()
        {
            List<Libro> listadoLibro = (from e in _BibliotecaContext.Libro select e).ToList();

            if (listadoLibro.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoLibro);
        }
    }
}
