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
            var modos = await _dao.ObtenerModoJuego(1); // o un método que traiga todos
            return View(modos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ModoJuego modoJuego)
        {
            if (ModelState.IsValid)
            {
                await _dao.AltaModoJuego(modoJuego);
                return RedirectToAction(nameof(Index));
            }
            return View(modoJuego);
        }
    }
}
