using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tarefa2.Web.Models;

namespace Tarefa2.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() =>
            RedirectToAction("Indice", "Galho", new { area = "Arvore" });

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
