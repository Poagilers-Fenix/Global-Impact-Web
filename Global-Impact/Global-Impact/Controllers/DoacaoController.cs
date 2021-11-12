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

        public DoacaoController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
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
            foreach (var i in itens) 
            { 
                Item item = i;
                doacaoItem.ItemId = i.ItemId;
                doacaoItem.Item = i;
            }

            IList<DoacaoItem> lista = HttpContext.Session
                .GetObjectFromJson<List<DoacaoItem>>("ListaDoacao");
            lista.Add(doacaoItem);
            HttpContext.Session.SetObjectAsJson("ListaDoacao", lista);
            return RedirectToAction("Cadastrar");
        }

        [HttpPost]
        public IActionResult Remover(int id)
        {
            Console.WriteLine(id);
            IList<DoacaoItem> lista = HttpContext.Session
                .GetObjectFromJson<List<DoacaoItem>>("ListaDoacao");
            foreach (var item in lista)
            {
                if (item.Item.ItemId == id)
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
