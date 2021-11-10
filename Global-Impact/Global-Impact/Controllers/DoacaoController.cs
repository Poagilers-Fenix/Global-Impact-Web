using Global_Impact.Persistence;
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
            //ViewBag.ongs = _context.ONGs.ToList();
            ViewBag.itens = _context.Itens.ToList();
            return View();
        }
    }
}
