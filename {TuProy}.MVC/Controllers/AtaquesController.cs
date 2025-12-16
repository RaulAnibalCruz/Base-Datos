using Microsoft.AspNetCore.Mvc;
using Bloody_Roar_2;
using Bloody_Roar_2.Persistencia;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySqlConnector; // ← Asegurate de tener este using (para MySqlException)

namespace _TuProy_.MVC.Controllers
{
    public class AtaquesController : Controller
    {
        private readonly IDao _dao;

        public AtaquesController(IDao dao)
        {
            _dao = dao;
        }

        // GET: Lista de ataques
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Login", "Account");

            var lista = await _dao.ObtenerAtaque();
            return View(lista);
        }

        // GET: Formulario crear ataque
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Login", "Account");

            await CargarPersonajes();
            return View();
        }

        // POST: Crear ataque con validaciones y captura del trigger
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Ataque ataque)
        {
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                await CargarPersonajes();
                return View(ataque);
            }

            // Validación de ataque duplicado
            var listaAtaques = await _dao.ObtenerAtaque();
            bool existe = listaAtaques.Any(a =>
                a.IdPersonaje == ataque.IdPersonaje &&
                a.Tipo_Ataque.ToLower().Trim() == ataque.Tipo_Ataque.ToLower().Trim()
            );

            if (existe)
            {
                ModelState.AddModelError("", "⚠ Este personaje ya posee un ataque de ese tipo.");
                await CargarPersonajes();
                return View(ataque);
            }

            // Intentamos insertar (aquí puede saltar el trigger)
            try
            {
                await _dao.AltaAtaque(ataque);
                return RedirectToAction("Index");
            }
            catch (MySqlException ex) when (ex.Message.Contains("menor o igual a 0"))
            {
                ModelState.AddModelError("Danio", "El daño del ataque no puede ser menor o igual a 0");
                ViewBag.Error = "El daño del ataque no puede ser menor o igual a 0";
                await CargarPersonajes();
                return View(ataque);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Error inesperado al crear el ataque.");
                ViewBag.Error = "Error inesperado al crear el ataque.";
                await CargarPersonajes();
                return View(ataque);
            }
            
        }

        // POST: Eliminar ataque
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            if (HttpContext.Session.GetInt32("IdUsuario") == null)
                return RedirectToAction("Login", "Account");

            await _dao.EliminarAtaque(id);
            return RedirectToAction("Index");
        }

        // MÉTODO AUXILIAR PARA NO REPETIR CÓDIGO
        private async Task CargarPersonajes()
        {
            var personajes = await _dao.ObtenerTodoPersonaje();
            ViewBag.Personajes = new SelectList(personajes, "IdPersonaje", "Nombre");
        }
    }
}