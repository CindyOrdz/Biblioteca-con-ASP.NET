using ASP_MVC_MYSQL.Data.DAO;
using ASP_MVC_MYSQL.Data.DTO;
using ASP_MVC_MYSQL.Models;


namespace ASP_MVC_MYSQL.Services
{
    public class LibroService
    {
        private readonly LibroDAO _libroDAO;

        public LibroService(LibroDAO libroDAO)
        {
            _libroDAO = libroDAO;
        }

        public async Task<List<LibroDTO>> Index()
        {
            List<Libro> listaLibros = await _libroDAO.Listar();
            var listaDTO = listaLibros.Select(x => LibroToDTO(x)).ToList();
            return listaDTO;
        }

        public static LibroDTO LibroToDTO(Libro libro) => new LibroDTO
        {
            Id = libro.Id,
            Titulo = libro.Titulo,
            Autor = libro.Autor,
            NumPag = libro.NumPag
        };

        public async Task Crear(LibroDTO libroDTO)
        {
            //Mapea las propiedades que vienen del DTO al modelo
            Libro lb = new Libro
            {
                Id = libroDTO.Id,
                Titulo = libroDTO.Titulo,
                Autor = libroDTO.Autor,
                NumPag = libroDTO.NumPag
            };
            await _libroDAO.Agregar(lb);
        }

        public async Task Editar(LibroDTO libroDTO)
        {
            //Mapea las propiedades que vienen del DTO al modelo
            Libro lb = new Libro
            {
                Id = libroDTO.Id,
                Titulo = libroDTO.Titulo,
                Autor = libroDTO.Autor,
                NumPag = libroDTO.NumPag
            };
            await _libroDAO.Editar(lb);
        }

        public async Task Eliminar(int id)
        {
            await _libroDAO.Borrar(id);
        }
    }
}
