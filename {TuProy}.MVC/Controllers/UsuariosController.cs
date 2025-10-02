using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using _TuProy_.MVC.Models;
using Bloody_Roar_2;
using Bloody_Roar_2.Persistencia;

namespace _TuProy_.MVC.Controllers

{
    public class UsuariosController : Controller
    {
        private readonly IDao _dao;

        public UsuariosController(IDao dao)
        {
            _dao = dao;
        }

        // GET: /Usuarios
        public async Task<IActionResult> Index()
        {
            var usuarios = await _dao.ObtenerTodoUsuario();
            return View(usuarios);
        }

        // GET: /Usuarios/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var usuario = await _dao.ObtenerUsuario(id);
            if (usuario == null) return NotFound();
            return View(usuario);
        }

        // GET: /Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                await _dao.AltaUsuario(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }
    }
}
