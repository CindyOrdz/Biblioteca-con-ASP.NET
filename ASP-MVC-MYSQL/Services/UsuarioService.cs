using ASP_MVC_MYSQL.Data.DAO;
using ASP_MVC_MYSQL.Data.DTO;
using ASP_MVC_MYSQL.Models;

namespace ASP_MVC_MYSQL.Services
{
    public class UsuarioService
    {
        private readonly UsuarioDAO _usuarioDAO;

        public UsuarioService(UsuarioDAO usuarioDAO)
        {
            _usuarioDAO = usuarioDAO;
        }


        public Usuario Login(UsuarioDTO usuarioDTO)
        {
            //Mapea las propiedades que vienen del DTO al modelo
            Usuario usuario = new Usuario
            {
                Correo = usuarioDTO.Correo,
                Contraseña = usuarioDTO.Contraseña
            };

            
            return usuario;
        }

        public async Task<Usuario> Validar(Usuario usuario)
        {
            var existeCorreo = await _usuarioDAO.ExistenciaCorreo(usuario.Correo);
            return existeCorreo;
        }

        public async Task Registro(UsuarioDTO usuarioDTO)
        {
            string HashClave = BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Contraseña);
            //Mapea las propiedades que vienen del DTO al modelo
            Usuario usuario = new Usuario
            {
                Correo = usuarioDTO.Correo,
                Contraseña = HashClave,
                Rol = "Socio"
            };
            await _usuarioDAO.Agregar(usuario);
        }

    }
}
