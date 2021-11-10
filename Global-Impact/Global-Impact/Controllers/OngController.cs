using Global_Impact.Persistence;
using Global_Impact.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            var listarOngs = _context.ONGs.Include(h => h.Endereco).ToList();
            return View(listarOngs);
        }

        public IActionResult PaginaOng()
        {
            return View();
        }

        public IActionResult Cadastrar()
        {
            return View();
        }
    }
}
