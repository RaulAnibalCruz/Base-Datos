using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using _TuProy_.MVC.Models;
using Bloody_Roar_2;
using Bloody_Roar_2.Persistencia;

namespace _TuProy_.MVC.Controllers

{
    public class AtaquesController : Controller
    {
        private readonly IDao _dao;

        public AtaquesController(IDao dao)
        {
            _dao = dao;
        }

        // ✅ Mostrar todos los ataques
        public async Task<IActionResult> Index()
        {
            var lista = await _dao.ObtenerAtaque(); // ← método que devuelve lista con nombre del personaje
            return View(lista);
        }

        // ✅ Formulario para crear un nuevo ataque
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            // Trae los personajes para mostrarlos en el <select>
            var personajes = await _dao.ObtenerTodoPersonaje();
            ViewBag.Personajes = personajes;
            return View();
        }

        // ✅ Alta del ataque en BD
        [HttpPost]
        public async Task<IActionResult> Crear(Ataque ataque)
        {
            if (!ModelState.IsValid)
            {
                var personajes = await _dao.ObtenerTodoPersonaje();
                ViewBag.Personajes = personajes;
                return View(ataque);
            }

            await _dao.AltaAtaque(ataque);
            return RedirectToAction("Index");
        }

        // ✅ Eliminar un ataque
        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _dao.EliminarAtaque(id);
            return RedirectToAction("Index");
        }
    }
}

