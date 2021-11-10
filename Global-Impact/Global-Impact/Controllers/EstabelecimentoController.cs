using Global_Impact.Models;
using Global_Impact.Persistence;
using Global_Impact.Repositories;
using Global_Impact.SessionHelpers;
using Microsoft.AspNetCore.Http;
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
        private IEstabelecimentoRepository _estabRepository;

        public EstabelecimentoController(WefeedContext context, IEstabelecimentoRepository estabRepository)
        {
            _context = context;
            _estabRepository = estabRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Estabelecimento estab)
        {
            _estabRepository.Cadastrar(estab);
            _estabRepository.Salvar();
            HttpContext.Session.SetObjectAsJson("EstabSessao", estab);
            TempData["msg"] = "Estabelecimento cadastrado com sucesso!";
            return RedirectToAction("Index", "home");
        }
    }
}
