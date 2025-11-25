using Microsoft.AspNetCore.Mvc;
using Bloody_Roar_2;
using Bloody_Roar_2.Persistencia;

namespace _TuProy_.MVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IDao _dao;

        public UsuariosController(IDao dao)
        {
            _dao = dao;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Login", "Account");

            var usuarios = await _dao.ObtenerTodoUsuario();
            return View(usuarios);
        }

        public IActionResult Crear()
        {
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Usuario usuario)
        {
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Login", "Account");

            if (string.IsNullOrWhiteSpace(usuario.Email) ||
                !usuario.Email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.Error = "El email es obligatorio y debe terminar en @gmail.com.";
                return View(usuario);
            }

            var existente = await _dao.BuscarUsuarioPorEmail(usuario.Email);
            if (existente != null)
            {
                ViewBag.Error = "El email ya está registrado.";
                return View(usuario);
            }

            var id = await _dao.AltaUsuario(usuario);
            if (id == -1)
            {
                ViewBag.Error = "El email ya está registrado.";
                return View(usuario);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Login", "Account");

            int idLogueado = HttpContext.Session.GetInt32("IdUsuario") ?? 0;

            if (id == idLogueado)
            {
                TempData["Error"] = "No puedes eliminar tu propio usuario.";
                return RedirectToAction("Index");
            }

            await _dao.EliminarUsuario(id);
            return RedirectToAction("Index");
        }
    }
}
