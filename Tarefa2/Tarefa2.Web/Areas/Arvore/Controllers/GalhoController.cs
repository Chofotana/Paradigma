using Microsoft.AspNetCore.Mvc;
using Tarefa2.Aplicacao;
using Tarefa2.Dominio.Entidades;
using Tarefa2.Dominio.Enums;

namespace Tarefa2.Web.Areas.Arvore.Controllers
{
    [Area("Arvore")]
    public class GalhoController : Controller
    {
        public GalhoController() =>
            ControladorGalho = new();

        private ControladorGalho ControladorGalho { get; set; }

        [HttpGet]
        public IActionResult Indice()
        {
            ViewBag.Array = string.Empty;
            ViewBag.Raiz = "-";
            ViewBag.GalhosEsquerda = "-";
            ViewBag.GalhosDireita = "-";
            return View(new List<Galho>());
        }

        [HttpPost]
        public IActionResult Indice(string array)
        {
            var galhos = ControladorGalho.ObterGalhos(array);

            ViewBag.Array = array;
            ViewBag.Raiz = galhos.First(galho => galho.Lado is null).Valor;
            ViewBag.GalhosEsquerda = ObterGalhosLado(LadoEnum.Esquerda);
            ViewBag.GalhosDireita = ObterGalhosLado(LadoEnum.Direita);

            return View(galhos);

            string ObterGalhosLado(LadoEnum lado)=>
                galhos.Any(galho => galho.Lado.Equals(lado)) 
                ? string.Join(", ", galhos.Where(galho => galho.Lado.Equals(lado)).Select(galho => galho.Valor).OrderByDescending(valor => valor)) 
                : "-";
        }
    }
}
