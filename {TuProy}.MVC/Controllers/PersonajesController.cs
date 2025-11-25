using Microsoft.AspNetCore.Mvc;
using Bloody_Roar_2;
using Bloody_Roar_2.Persistencia;

namespace _TuProy_.MVC.Controllers
{
    public class PersonajesController : Controller
    {
        private readonly IDao _dao;

        public PersonajesController(IDao dao)
        {
            _dao = dao;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Login", "Account");

            var personajes = await _dao.ObtenerTodoPersonaje();
            return View(personajes);
        }

        public IActionResult Crear()
        {
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Personaje personaje)
        {
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Login", "Account");

            await _dao.AltaPersonaje(personaje);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Login", "Account");

            await _dao.EliminarPersonaje(id);
            return RedirectToAction("Index");
        }
    }
}
