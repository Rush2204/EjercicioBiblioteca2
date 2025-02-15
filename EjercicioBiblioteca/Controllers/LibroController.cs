using EjercicioBiblioteca.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // Para buscar los registros por ID

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            Libro? libro = (from e in _BibliotecaContext.Libro where e.id_libro == id select e).FirstOrDefault();

            if (libro == null)
            {
                return NotFound();
            }
            return Ok(libro);
        }

        // Para agregar un nuevo registro:

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarLibro([FromBody] Libro libro)
        {
            try
            {
                _BibliotecaContext.Libro.Add(libro);
                _BibliotecaContext.SaveChanges();
                return Ok(libro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Para actualizar un registro

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarLibro(int id, [FromBody] Libro libroModificar)
        {
            Libro? libroActual = (from e in _BibliotecaContext.Libro where e.id_libro == id select e).FirstOrDefault();

            if (libroActual == null)
            { return NotFound(); }

            libroActual.titulo = libroModificar.titulo;
            libroActual.anioPublicacion = libroModificar.anioPublicacion;
            libroActual.autor_id = libroModificar.autor_id;
            libroActual.categoria_id = libroModificar.categoria_id;
            libroActual.resumen = libroModificar.resumen;

            _BibliotecaContext.Entry(libroActual).State = EntityState.Modified;
            _BibliotecaContext.SaveChanges();

            return Ok(libroModificar);
        }
    }

}
