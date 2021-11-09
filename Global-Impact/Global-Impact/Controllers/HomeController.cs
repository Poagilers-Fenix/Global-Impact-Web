using Global_Impact.Models;
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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //ViewBag.titles = new string[] { "Fome Zero e Agricultura Sustentável", "20 milhões de brasileiros sentem fome", "Por que o desperdício gera fome?", "Desperdício de toneladas de alimentos", "Reduzir o desperdício com tecnologia", "Ativismo contra a fome?" };
            //ViewBag.links = new string[] { "https://brasil.un.org/pt-br/sdgs/2", "https://www.redebrasilatual.com.br/cidadania/2021/10/fome-brasil-19-milhoes-inseguranca-alimentar/", "https://www.fiap.com.br", "https://www.netflix.com.br", "https://www.youtube.com", "https://conferenciassan.org.br/ativismo-contra-a-fome/" };
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult NovaDoacao()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
