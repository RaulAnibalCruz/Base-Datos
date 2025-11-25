using Microsoft.AspNetCore.Mvc;
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

        // GET /Combates/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Personajes = await _dao.ObtenerTodoPersonaje();
            ViewBag.Modos = await _dao.ObtenerTodoModoJuego();
            ViewBag.Usuarios = await _dao.ObtenerTodoUsuario();
            return View();
        }

        // POST /Combates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Combate combate)
        {
            if (ModelState.IsValid)
            {
                await _dao.AltaCombate(combate);
                return RedirectToAction("Details", new { id = combate.IdCombate });
            }

            ViewBag.Personajes = await _dao.ObtenerTodoPersonaje();
            ViewBag.Modos = await _dao.ObtenerTodoModoJuego();
            ViewBag.Usuarios = await _dao.ObtenerTodoUsuario();

            return View(combate);
        }

        // GET /Combates/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var combate = await _dao.ObtenerCombatePorId(id);
            if (combate == null) return NotFound();
            return View(combate);
        }
    }
}
