using System.ComponentModel.DataAnnotations;

namespace ASP_MVC_MYSQL.Data.DTO
{
    public class LibroDTO
    {
        [Display(Name = "ID Libro")]
        [Required(ErrorMessage = "El ID del libro es obligatorio")]
        public int? Id { get; set; }

        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "El titulo del libro es obligatorio")]
        [StringLength(250, ErrorMessage = "El titulo solo puede contener 250 caracteres")]
        public string Titulo { get; set; }

        [Display(Name = "Nombre autor")]
        [StringLength(250, ErrorMessage = "El nombre del autor solo puede contener 250 caracteres")]
        [Required(ErrorMessage = "El autor del libro es obligatorio")]
        public string Autor { get; set; }

        [Display(Name = "Número de páginas")]
        [Required(ErrorMessage = "El número de paginas del libro es obligatorio")]
        public int? NumPag { get; set; }
    }
}
