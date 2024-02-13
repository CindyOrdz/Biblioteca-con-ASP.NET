using System.ComponentModel.DataAnnotations;


namespace ASP_MVC_MYSQL.Data.DTO
{
    public class UsuarioDTO
    {
        [Display(Name = "Correo electronico")]
        [Required(ErrorMessage = "El correo es obligatorio")]
        public string Correo { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Contraseña { get; set; }

        public string Rol { get; set; }

    }
}
