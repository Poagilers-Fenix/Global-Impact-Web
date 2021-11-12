using Global_Impact.Persistence;
using Global_Impact.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Global_Impact.Repositories;

namespace Global_Impact.Controllers
{
    public class OngController : Controller
    {
        private IOngRepository _repository;

        public OngController(IOngRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            IList<Ong> listaOng = _repository.Listar();
            return View(listaOng);
        }

        public IActionResult PaginaOng(int id)
        {
            Ong ong = _repository.BuscarPorId(id);
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
            _repository.Cadastrar(ong);
            _repository.Salvar();
            TempData["Sucesso"] = "Ong cadastrada com sucesso!";
            return View();
        }

        public IActionResult DeletarOng()
        {
            return View();
        }
    }
}
