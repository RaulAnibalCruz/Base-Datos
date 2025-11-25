using Microsoft.AspNetCore.Mvc;
using Bloody_Roar_2;
using Bloody_Roar_2.Persistencia;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _TuProy_.MVC.Controllers
{
    public class AtaquesController : Controller
    {
        private readonly IDao _dao;

        public AtaquesController(IDao dao)
        {
            _dao = dao;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Login", "Account");

            var lista = await _dao.ObtenerAtaque();
            return View(lista);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Login", "Account");

            var personajes = await _dao.ObtenerTodoPersonaje();
            ViewBag.Personajes = new SelectList(personajes, "IdPersonaje", "Nombre");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Ataque ataque)
        {
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                var personajes = await _dao.ObtenerTodoPersonaje();
                ViewBag.Personajes = new SelectList(personajes, "IdPersonaje", "Nombre");
                return View(ataque);
            }

            var listaAtaques = await _dao.ObtenerAtaque();

            bool existe = listaAtaques.Any(a =>
                a.IdPersonaje == ataque.IdPersonaje &&
                a.Tipo_Ataque.ToLower().Trim() == ataque.Tipo_Ataque.ToLower().Trim()
            );

            if (existe)
            {
                ModelState.AddModelError("", "⚠ Este personaje ya posee un ataque de ese tipo.");
                var personajes = await _dao.ObtenerTodoPersonaje();
                ViewBag.Personajes = new SelectList(personajes, "IdPersonaje", "Nombre");
                return View(ataque);
            }

            await _dao.AltaAtaque(ataque);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Login", "Account");

            await _dao.EliminarAtaque(id);
            return RedirectToAction("Index");
        }
    }
}
