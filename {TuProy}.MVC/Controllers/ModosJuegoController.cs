using Microsoft.AspNetCore.Mvc;
using Bloody_Roar_2;
using Bloody_Roar_2.Persistencia;

namespace _TuProy_.MVC.Controllers
{
    public class ModosJuegoController : Controller
    {
        private readonly IDao _dao;

        public ModosJuegoController(IDao dao)
        {
            _dao = dao;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Login", "Account");

            var modos = await _dao.ObtenerTodoModoJuego();
            return View(modos);
        }

        public IActionResult Crear()
        {
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ModoJuego modo)
        {
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Login", "Account");

            await _dao.AltaModoJuego(modo);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Login", "Account");

            await _dao.EliminarModoJuego(id);
            return RedirectToAction("Index");
        }
    }
}
