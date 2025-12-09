using Microsoft.AspNetCore.Mvc;
using Bloody_Roar_2;
using Bloody_Roar_2.Persistencia;

namespace _TuProy_.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IDao _dao;

        public AccountController(IDao dao)
        {
            _dao = dao;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuario model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Contrasenia))
            {
                ViewBag.Error = "Email y contraseña son obligatorios.";
                return View();
            }

            var usuario = await _dao.BuscarUsuarioPorEmail(model.Email);
            if (usuario == null)
            {
                ViewBag.Error = "Email o contraseña incorrectos.";
                return View();
            }

            string hash = SHA256(model.Contrasenia);
            if (usuario.Contrasenia != hash)
            {
                ViewBag.Error = "Email o contraseña incorrectos.";
                return View();
            }

            HttpContext.Session.SetInt32("IdUsuario", usuario.IdUsuario);
            HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            // Validación de formato de email (AHORA SÍ BIEN ESCRITA)
                    if (string.IsNullOrWhiteSpace(usuario.Email) ||
                !usuario.Email.Contains("@") ||
                usuario.Email.EndsWith("@") ||
                usuario.Email.LastIndexOf(".") <= usuario.Email.IndexOf("@") + 1)
            {
                ViewBag.Error = "El email debe tener un formato válido (ej: nombre@dominio.com)";
                return View(usuario);
            }

            // ← este corchete cierra el if correctamente

            var existe = await _dao.BuscarUsuarioPorEmail(usuario.Email);
            if (existe != null)
            {
                ViewBag.Error = "Ya existe un usuario con ese email.";
                return View(usuario);
            }

            int id = await _dao.AltaUsuario(usuario);
            if (id <= 0)
            {
                ViewBag.Error = "Error al registrar. Intenta de nuevo.";
                return View(usuario);
            }

            HttpContext.Session.SetInt32("IdUsuario", id);
            HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        private string SHA256(string texto)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            byte[] bytes = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(texto));
            return Convert.ToHexString(bytes).ToLower();
        }
    }
}