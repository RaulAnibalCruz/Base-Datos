using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using _TuProy_.MVC.Models;
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

        // Mostrar todos los ataques
        public async Task<IActionResult> Index()
        {
            var lista = await _dao.ObtenerAtaque();
            return View(lista);
        }

        // Formulario GET para crear un ataque
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var personajes = await _dao.ObtenerTodoPersonaje();
            ViewBag.Personajes = new SelectList(personajes, "IdPersonaje", "Nombre");
            return View();
        }

        // Alta en BD
        [HttpPost]
    public async Task<IActionResult> Crear(Ataque ataque)
    {
        if (!ModelState.IsValid)
        {
            var personajes = await _dao.ObtenerTodoPersonaje();
            ViewBag.Personajes = new SelectList(personajes, "IdPersonaje", "Nombre");
            return View(ataque);
        }

        // 🔥 VALIDACIÓN: evitar ataques repetidos para el mismo personaje
        var listaAtaques = await _dao.ObtenerAtaque();

        bool existe = listaAtaques.Any(a =>
            a.IdPersonaje == ataque.IdPersonaje &&
            a.Tipo_Ataque.ToLower().Trim() == ataque.Tipo_Ataque.ToLower().Trim()
        );

        if (existe)
        {
            ModelState.AddModelError("", "⚠ Este personaje ya posee un ataque de ese tipo. No se puede repetir.");
            var personajes = await _dao.ObtenerTodoPersonaje();
            ViewBag.Personajes = new SelectList(personajes, "IdPersonaje", "Nombre");
            return View(ataque);
        }

        await _dao.AltaAtaque(ataque);
        return RedirectToAction("Index");
    }

        

        // Eliminar ataque
        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _dao.EliminarAtaque(id);
            return RedirectToAction("Index");
        }
    }
}
