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

        public async Task<IActionResult> Details(int id)
        {
            var ataque = await _dao.ObtenerAtaque(id);
            if (ataque == null) return NotFound();
            return View(ataque);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ataque ataque)
        {
            if (ModelState.IsValid)
            {
                await _dao.AltaAtaque(ataque);
                return RedirectToAction("Details", new { id = ataque.IdAtaque });
            }
            return View(ataque);
        }
    }
}
