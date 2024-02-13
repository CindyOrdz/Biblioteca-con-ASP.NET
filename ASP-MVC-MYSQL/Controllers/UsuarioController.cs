using Microsoft.AspNetCore.Mvc;
using ASP_MVC_MYSQL.Data.DTO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using ASP_MVC_MYSQL.Services;

namespace ASP_MVC_MYSQL.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UsuarioDTO usuarioDTO)
        {
            if (!ModelState.IsValid) return View();

            ////Mapea las propiedades que vienen del DTO al modelo
            //Usuario usuario = new Usuario
            //{
            //    Correo = usuarioDTO.Correo,
            //    Contraseña = usuarioDTO.Contraseña
            //};

            //var existeCorreo = await _usuarioDAO.ExistenciaCorreo(usuario.Correo);

            var user = _usuarioService.Login(usuarioDTO);

            var existe = await _usuarioService.Validar(user);

            
            if (existe != null)
            {
                bool ClaveCorrecta = BCrypt.Net.BCrypt.Verify(user.Contraseña, existe.Contraseña);
                if (ClaveCorrecta){
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, existe.Correo),
                    new Claim(ClaimTypes.Role, existe.Rol)
                };


                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Bienvenida", "Usuario");

                }
                else
                {
                    TempData["ErrorLogin"] = "Contraseña incorrecta, intente nuevamente.";
                    return View();
                }
            }
            else {
                TempData["ErrorLogin"] = "El correo ingresado no se encuentra registrado.";
                return View();
            }


        }


        public async Task<IActionResult> Salir()
        {
            //Elimina la identificación y la información de autenticación asociada con la cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Usuario");
        }

        public IActionResult RegistroSocio()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistroSocio(UsuarioDTO usuarioDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(usuarioDTO);
                }

                //string HashClave = BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Contraseña);
                ////Mapea las propiedades que vienen del DTO al modelo
                //Usuario usuario = new Usuario
                //{
                //    Correo = usuarioDTO.Correo,
                //    Contraseña = HashClave,
                //    Rol = "Socio"
                //};
                //await _usuarioDAO.Agregar(usuario);
                await _usuarioService.Registro(usuarioDTO);

                TempData["RegistroExitoso"] = "¡Registro exitoso! Inicie sesión con las credenciales creadas";
                return RedirectToAction("Login", "Usuario");
            }
            catch (Exception ex)
            {
                TempData["ErrorAgregar"] = ex.Message;
                return View(usuarioDTO);
            }
        } 

        public IActionResult Restringido()
        {
            return View();
        }

        public IActionResult Bienvenida()
        {
            return View();
        }
    }
}
