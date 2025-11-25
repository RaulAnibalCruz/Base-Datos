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

        public async Task<IActionResult> Index()
        {
            var usuarios = await _dao.ObtenerTodoUsuario();
            return View(usuarios);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear (Usuario usuario)
        {
            // 1) Validación simple: email no vacío y termina en @gmail.com
            if (string.IsNullOrWhiteSpace(usuario.Email) || !usuario.Email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.Error = "El email es obligatorio y debe terminar en @gmail.com.";
                return View(usuario);
            }

            // 2) Evitar duplicados: llamamos al SP desde el DAO (o al método Buscar)
            var existente = await _dao.BuscarUsuarioPorEmail(usuario.Email);
            if (existente != null)
            {
                ViewBag.Error = "El email ya está registrado.";
                return View(usuario);
            }

            // 3) Llamar AltaUsuario (retorna id o -1)
            var id = await _dao.AltaUsuario(usuario);
            if (id == -1)
            {
                ViewBag.Error = "El email ya está registrado.";
                return View(usuario);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            await _dao.EliminarUsuario(id);
            return RedirectToAction("Index");
        }
    }

}


