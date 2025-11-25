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

        // =============================
        //      INDEX - Listar Combates
        // =============================
        public async Task<IActionResult> Index()
        {
            var combates = await _dao.ObtenerTodoCombate();
            return View(combates);
        }

        // =============================
        //      CREATE GET
        // =============================
        public async Task<IActionResult> Create()
        {
            ViewBag.Personajes = await _dao.ObtenerTodoPersonaje();
            ViewBag.Modos = await _dao.ObtenerTodoModoJuego();
            ViewBag.Usuarios = await _dao.ObtenerTodoUsuario();
            return View();
        }

        // =============================
        //      CREATE POST
        // =============================
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

        // =============================
        //      DETAILS
        // =============================
        public async Task<IActionResult> Details(int id)
        {
            var combate = await _dao.ObtenerCombatePorId(id);
            if (combate == null) return NotFound();
            return View(combate);
        }

        // =============================
        //      ELIMINAR
        // =============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _dao.EliminarCombate(id);
            return RedirectToAction("Index");
        }
    }
}
