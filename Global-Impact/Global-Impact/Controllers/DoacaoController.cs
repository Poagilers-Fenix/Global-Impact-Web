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
        private IDoacaoRepository _doacaoRepository;

        public DoacaoController(IItemRepository itemRepository, IDoacaoRepository doacaorepository)
        {
            _itemRepository = itemRepository;
            _doacaoRepository = doacaorepository;
        }

        public IActionResult Index()
        {
            ViewBag.doacoes = _doacaoRepository.Listar();
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
                i.Nome.Contains(nome) || nome == null);
            ViewBag.lista = lista;
            return View();
        }

        [HttpPost]
        public IActionResult Adicionar(DoacaoItem doacaoItem, int id)
        {
            doacaoItem.ItemId = id;

            IList<DoacaoItem> lista = HttpContext.Session
                .GetObjectFromJson<List<DoacaoItem>>("ListaDoacao");
            lista.Add(doacaoItem);
            HttpContext.Session.SetObjectAsJson("ListaDoacao", lista);
            return RedirectToAction("Cadastrar");
        }

        [HttpPost]
        public IActionResult Remover(int id)
        {
            IList<DoacaoItem> lista = HttpContext.Session
                .GetObjectFromJson<List<DoacaoItem>>("ListaDoacao");
            foreach (var item in lista)
            {
                if (item.ItemId == id)
                {
                    lista.Remove(item);
                    HttpContext.Session.SetObjectAsJson("ListaDoacao", lista);
                    break;
                }
            }
            return RedirectToAction("Cadastrar");
        }
    }
}
