using Global_Impact.Persistence;
using Global_Impact.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Global_Impact.Controllers
{
    public class OngController : Controller
    {
        private WefeedContext _context;

        public OngController(WefeedContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IList<Ong> listaOng = _context.ONGs.ToList();
            return View(listaOng);
        }

        public IActionResult PaginaOng(Ong ong)
        {
            return View(ong);
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Ong ong)
        {
            if (!ModelState.IsValid)
            {
                TempData["Erro"] = "Erro ao cadastrar a ong, verifique se as informações estão corretas.";
                return View();
            }
            _context.ONGs.Add(ong);
            _context.SaveChanges();
            TempData["Sucesso"] = "Ong cadastrada com sucesso!";
            return View();
        }
    }
}
