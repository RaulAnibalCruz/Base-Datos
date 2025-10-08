using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using _TuProy_.MVC.Models;
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
            var modos = await _dao.ObtenerTodoModoJuego();
            return View(modos);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ModoJuego modo)
        {
            await _dao.AltaModoJuego(modo);
            return RedirectToAction("Index");
        }
    }
}

