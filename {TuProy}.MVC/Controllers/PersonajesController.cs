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

        public async Task<IActionResult> Index()
        {
            var personajes = await _dao.ObtenerTodoPersonaje();
            return View(personajes);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Personaje personaje)
        {
            await _dao.AltaPersonaje(personaje);
            return RedirectToAction("Index");
        }


            public async Task<IActionResult> Eliminar(int id)
    {
        await _dao.EliminarPersonaje(id);
        return RedirectToAction("Index"); // vuelve a la lista de personajes
    }

    }
}
