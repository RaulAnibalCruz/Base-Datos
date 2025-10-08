using Microsoft.AspNetCore.Mvc;

namespace Bloody_Roar_2.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); // Vista principal de apertura
        }
    }
}
