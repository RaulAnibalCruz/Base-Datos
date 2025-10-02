using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using _TuProy_.MVC.Models;
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

        // GET: /Personajes
        public async Task<IActionResult> Index()
        {
            var personajes = await _dao.ObtenerTodoPersonaje();
            return View(personajes);
        }

        // GET: /Personajes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var personaje = await _dao.ObtenerPersonaje(id);
            if (personaje == null) return NotFound();
            return View(personaje);
        }

        // GET: /Personajes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Personajes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Personaje personaje)
        {
            if (ModelState.IsValid)
            {
                await _dao.AltaPersonaje(personaje);
                return RedirectToAction(nameof(Index));
            }
            return View(personaje);
        }
    }
}
