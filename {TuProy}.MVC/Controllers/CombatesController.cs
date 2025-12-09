using Microsoft.AspNetCore.Mvc;
using Bloody_Roar_2;
using Bloody_Roar_2.Persistencia;

namespace _TuProy_.MVC.Controllers
{
    public class CombatesController : Controller
    {
        private readonly IDao _dao;

        public CombatesController(IDao dao) => _dao = dao;

        // INDEX - Muestra nombres
        public async Task<IActionResult> Index()
        {
            var combates = await _dao.ObtenerTodosCombatesConNombres();
            return View(combates);
        }

        // CREATE GET
        public async Task<IActionResult> Create()
        {
            ViewBag.Personajes = await _dao.ObtenerTodoPersonaje();
            ViewBag.Usuarios   = await _dao.ObtenerTodoUsuario();
            ViewBag.Modos      = await _dao.ObtenerTodoModoJuego();
            return View();
        }

        // CREATE POST
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
            ViewBag.Usuarios   = await _dao.ObtenerTodoUsuario();
            ViewBag.Modos      = await _dao.ObtenerTodoModoJuego();
            return View(combate);
        }

        // DETAILS - ÚNICO Y FUNCIONAL
        public async Task<IActionResult> Details(int id)
        {
            var combate = await _dao.ObtenerCombateConNombres(id);
            if (combate == null) return NotFound();
            return View(combate);
        }

        // ELIMINAR
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            await _dao.EliminarCombate(id);
            return RedirectToAction("Index");
        }
    }
}