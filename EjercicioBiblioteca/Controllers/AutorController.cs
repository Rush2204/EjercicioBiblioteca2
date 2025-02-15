﻿using Microsoft.AspNetCore.Http;
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
            try
            {
                List<Autor> listadoAutor = _BibliotecaContext.Autor.ToList();

                if (listadoAutor.Count == 0)
                {
                    return NotFound("No se encontraron autores.");
                }
                return Ok(listadoAutor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new { message = "Ocurrió un error al obtener los autores.", error = ex.Message });
            }
        }

        // Para buscar los registros por ID

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            Autor? autor = (from e in _BibliotecaContext.Autor where e.id_Autor == id select e).FirstOrDefault();

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


    }
}
