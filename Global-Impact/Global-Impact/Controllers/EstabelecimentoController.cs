using Global_Impact.Models;
using Global_Impact.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Impact.Controllers
{
    public class EstabelecimentoController : Controller
    {
        private WefeedContext _context;

        public EstabelecimentoController(WefeedContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        public IActionResult Cadastrar(Estabelecimento estab)
        {
            _context.Estabelecimentos.Add(estab);
            _context.SaveChanges();
            TempData["msg"] = "Estabelecimento cadastrado com sucesso!";
            return RedirectToAction("Cadastrar");
        }
    }
}
