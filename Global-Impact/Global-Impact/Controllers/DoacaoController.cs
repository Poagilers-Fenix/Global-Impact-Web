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
    public class DoacaoController : Controller
    {

        private IItemRepository _itemRepository;
        private IOngRepository _ongRepository;

        public DoacaoController(IItemRepository itemRepository, IOngRepository ongRepository)
        {
            _itemRepository = itemRepository;
            _ongRepository = ongRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastrar(string nome)
        {
            IList<DoacaoItem> lista = HttpContext.Session
                .GetObjectFromJson<List<DoacaoItem>>("ListaDoacao");
            if (lista == null) 
            { 
                lista = new List<DoacaoItem>();
                HttpContext.Session.SetObjectAsJson("ListaDoacao", lista);
            }
            ViewBag.itens = _itemRepository.BuscarPor(i => 
                i.Nome.ToLower().Contains(nome) || nome == null);
            ViewBag.lista = lista;
            return View();
        }

        [HttpPost]
        public IActionResult Adicionar(DoacaoItem doacaoItem)
        {
            IList<Item> itens = _itemRepository.BuscarPor(i => i.Nome == doacaoItem.Item.Nome);
            foreach (var i in itens) { Item item = i; doacaoItem.Item = item; }


            IList<DoacaoItem> lista = HttpContext.Session
                .GetObjectFromJson<List<DoacaoItem>>("ListaDoacao");
            lista.Add(doacaoItem);
            HttpContext.Session.SetObjectAsJson("ListaDoacao", lista);
            return RedirectToAction("Cadastrar");
        }

        [HttpGet]
        public IActionResult EscolherOng()
        {
            return View(_ongRepository.Listar());
        }

        [HttpPost]
        public IActionResult Remover(int id)
        {
            return RedirectToAction("Cadastrar");
        }
    }
}
