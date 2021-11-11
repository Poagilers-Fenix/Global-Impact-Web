using Global_Impact.Models;
using Global_Impact.Persistence;
using Global_Impact.SessionHelpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Impact.Controllers
{
    public class DoacaoController : Controller
    {

        private WefeedContext _context;

        public DoacaoController(WefeedContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastrar()
        {
            IList<DoacaoItem> lista = HttpContext.Session
                .GetObjectFromJson<List<DoacaoItem>>("ListaDoacao");
            if (lista == null) 
            { 
                lista = new List<DoacaoItem>();
                HttpContext.Session.SetObjectAsJson("ListaDoacao", lista);
            }
            ViewBag.itens = _context.Itens.ToList();
            ViewBag.lista = lista;
            return View();
        }

        [HttpPost]
        public IActionResult Adicionar(DoacaoItem doacaoItem)
        {
            IList<DoacaoItem> lista = HttpContext.Session
                .GetObjectFromJson<List<DoacaoItem>>("ListaDoacao");
            lista.Add(doacaoItem);
            HttpContext.Session.SetObjectAsJson("ListaDoacao", lista);
            return RedirectToAction("Cadastrar");
        }

        [HttpPost]
        public IActionResult Remover(int id)
        {
            return RedirectToAction("Cadastrar");
        }
    }
}
