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

        // =============================
        //      LOGIN GET
        // =============================
        public IActionResult Login()
        {
            return View();
        }

        // =============================
        //      LOGIN POST
        // =============================
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

        // =============================
        //         REGISTRO GET
        // =============================
        public IActionResult Registrar()
        {
            return View();
        }

        // =============================
        //         REGISTRO POST
        // =============================
        [HttpPost]
        public async Task<IActionResult> Registrar(Usuario usuario)
        {
            // Validar email
            if (!usuario.Email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.Error = "El email debe terminar en @gmail.com.";
                return View(usuario);
            }

            // Email repetido
            var existe = await _dao.BuscarUsuarioPorEmail(usuario.Email);
            if (existe != null)
            {
                ViewBag.Error = "Ya existe un usuario con ese email.";
                return View(usuario);
            }



            // Alta
            int id = await _dao.AltaUsuario(usuario);

            if (id == -1)
            {
                ViewBag.Error = "El email ya está registrado.";
                return View(usuario);
            }

            // Registrar y loguear automáticamente
            HttpContext.Session.SetInt32("IdUsuario", id);
            HttpContext.Session.SetString("UsuarioNombre", usuario.Nombre);

            return RedirectToAction("Index", "Home");
        }

        // =============================
        //        CERRAR SESIÓN
        // =============================
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // =============================
        //        HASH SHA-256
        // =============================
        private string SHA256(string texto)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            byte[] bytes = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(texto));
            return Convert.ToHexString(bytes).ToLower();
        }
    }
}
