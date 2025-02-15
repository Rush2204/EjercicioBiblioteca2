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

        // Para buscar los registros por ID con Autor

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            var libro = (from e in _BibliotecaContext.Libro 
                         join t in _BibliotecaContext.Autor
                         on e.autor_id equals t.id_Autor
                         where e.id_libro == id 
                         select new 
                         {
                             e.id_libro,
                             e.titulo,
                             e.anioPublicacion,
                             e.autor_id,
                             e.categoria_id,
                             e.resumen,
                             detalle = $"Autor : {t.Nombre}"
                         }).ToList();

            if (libro == null)
            {
                return NotFound();
            }
            return Ok(libro);
        }

        /*
         // Para buscar los registros por ID del autor y que en un solo registro se vea todos los libros

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            var autor = _BibliotecaContext.Autor
        .Where(e => e.id_autor == id)
        .Select(e => new
        {
            e.id_autor,
            e.Nombre,
            e.Nacionalidad,
            Libros = _BibliotecaContext.Libro
                .Where(t => t.autor_id == e.id_autor)
                .Select(t => t.titulo)  // Solo extraemos los títulos
                .ToList() // Convertimos a lista
        })
        .FirstOrDefault();

            if (autor == null)
            {
                return NotFound();
            }
            return Ok(autor);
        }
        */

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

        // Para Eliminar un registro

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarLibro(int id)
        {
            Libro? libro = (from e in _BibliotecaContext.Libro where e.id_libro == id select e).FirstOrDefault();

            if (libro == null)
            { return NotFound(); }

            _BibliotecaContext.Libro.Attach(libro);
            _BibliotecaContext.Libro.Remove(libro);
            _BibliotecaContext.SaveChanges();

            return Ok(libro);
        }
    }

}
