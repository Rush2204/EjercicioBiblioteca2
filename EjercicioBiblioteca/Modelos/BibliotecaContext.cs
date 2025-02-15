using EjercicioBiblioteca.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace WebApplication1.Modelos
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options)
        {
        }

        public DbSet<Autor> Autor { get; set; }
        public DbSet<Libro> Libro { get; set; }
    }
}
