using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using _TuProy_.MVC.Models;
using Bloody_Roar_2;
using Bloody_Roar_2.Persistencia;

namespace _TuProy_.MVC.Controllers

{
    public class CombatesController : Controller
    {
        private readonly IDao _dao;

        public CombatesController(IDao dao)
        {
            _dao = dao;
        }

        public async Task<IActionResult> Details(int id)
        {
            var combate = await _dao.ObtenerCombatePorId(id);
            if (combate == null) return NotFound();
            return View(combate);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Combate combate)
        {
            if (ModelState.IsValid)
            {
                await _dao.AltaCombate(combate);
                return RedirectToAction("Details", new { id = combate.IdCombate });
            }
            return View(combate);
        }
    }
}
