using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Modelos;
using Microsoft.EntityFrameworkCore;
using EjercicioBiblioteca.Modelos;

namespace EjercicioBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : Controller
    {
        private readonly BibliotecaContext _BibliotecaContext;

        public AutorController(BibliotecaContext BibliotecaContext)
        {
            _BibliotecaContext = BibliotecaContext;
        }

        // Para poder ver todos los registros:

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Index()
        {
            List<Autor> listadoAutor = (from e in _BibliotecaContext.Autor select e).ToList();

            if (listadoAutor.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoAutor);
        }


    }
}
