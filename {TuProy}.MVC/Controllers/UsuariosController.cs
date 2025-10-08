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

        // Lista de usuarios
        public async Task<IActionResult> Index()
        {
            var usuarios = await _dao.ObtenerTodoUsuario();
            return View(usuarios);
        }

        // Alta usuario
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Usuario usuario)
        {
            await _dao.AltaUsuario(usuario);
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Eliminar(int id)
        {
            await _dao.EliminarUsuario(id);
            return RedirectToAction("Index"); // vuelve a la lista de usuarios
        }

    }
}


