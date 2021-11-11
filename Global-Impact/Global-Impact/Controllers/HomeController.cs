using Global_Impact.Models;
using Global_Impact.Persistence;
using Global_Impact.Repositories;
using Global_Impact.SessionHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Impact.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private WefeedContext _context;
        private IEstabelecimentoRepository _estabRepository;

        public HomeController(ILogger<HomeController> logger, WefeedContext context, IEstabelecimentoRepository estabRepository)
        {
            _logger = logger;
            _context = context;
            _estabRepository = estabRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Estabelecimento estab)
        {
            Estabelecimento estabEncontrado = _estabRepository.BuscarPor(e => e.Email == estab.Email);
            if (estabEncontrado != null)
            {
                if (estabEncontrado.Senha == estab.Senha)
                {
                    HttpContext.Session.SetObjectAsJson("EstabSessao", estab);
                    return RedirectToAction("index");
                }
            }
            TempData["Erro"] = "E-mail ou senha incorretos!";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
