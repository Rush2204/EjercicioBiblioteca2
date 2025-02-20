using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        // Para buscar los registros por ID con libro

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            var autor = (from e in _BibliotecaContext.Autor
                         join t in _BibliotecaContext.Libro
                         on e.id_Autor equals t.autor_id
                         where e.id_Autor == id
                         select new
                         {
                             e.id_Autor,
                             e.Nombre,
                             e.Nacionalidad,
                             libros = (from e in _BibliotecaContext.Autor
                                       join t in _BibliotecaContext.Libro
                                       on e.id_Autor equals t.autor_id
                                       where e.id_Autor == id
                                       select new
                                       {
                                           t.titulo
                                       }).ToList()
                         }).FirstOrDefault();

            if (autor == null)
            {
                return NotFound();
            }
            return Ok(autor);
        }

        // Para agregar un nuevo registro:

        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarAutor([FromBody] Autor autor)
        {
            try
            {
                _BibliotecaContext.Autor.Add(autor);
                _BibliotecaContext.SaveChanges();
                return Ok(autor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Para actualizar un registro

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarAutor(int id, [FromBody] Autor autorModificar)
        {
            Autor? autorActual = (from e in _BibliotecaContext.Autor where e.id_Autor == id select e).FirstOrDefault();

            if (autorActual == null)
            { return NotFound(); }

            autorActual.Nombre = autorModificar.Nombre;
            autorActual.Nacionalidad = autorModificar.Nacionalidad;

            _BibliotecaContext.Entry(autorActual).State = EntityState.Modified;
            _BibliotecaContext.SaveChanges();

            return Ok(autorModificar);
        }

        // Para Eliminar un registro

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarAutor(int id)
        {
            Autor? autor = (from e in _BibliotecaContext.Autor where e.id_Autor == id select e).FirstOrDefault();

            if (autor == null)
            { return NotFound(); }

            _BibliotecaContext.Autor.Attach(autor);
            _BibliotecaContext.Autor.Remove(autor);
            _BibliotecaContext.SaveChanges();

            return Ok(autor);
        }

    }

}
