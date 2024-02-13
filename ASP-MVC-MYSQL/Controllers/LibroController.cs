using ASP_MVC_MYSQL.Data.DTO;
using ASP_MVC_MYSQL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP_MVC_MYSQL.Controllers
{
    //[Authorize]
    public class LibroController : Controller
    {
        private readonly LibroService _libroService;

        public LibroController(LibroService libroService)
        {
            _libroService = libroService;
        }

        public async Task<IActionResult> Index()
        {
            var listaDTO = await _libroService.Index();
            return View(listaDTO);
        }

   
        [Authorize(Roles = "Administrador")]
        public IActionResult Crear()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Crear(LibroDTO libroDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(libroDTO);
                }

                await _libroService.Crear(libroDTO);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorAgregar"] = ex.Message;
                return View(libroDTO);
            }
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Eliminar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                await _libroService.Eliminar(id);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["ErrorEliminar"] = ex.Message;
                return View();
            }
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Editar()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Editar(LibroDTO libroDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(libroDTO);
                }

                await _libroService.Editar(libroDTO);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorEditar"] = ex.Message;
                return View(libroDTO);
            }
        }

        


    }
}

