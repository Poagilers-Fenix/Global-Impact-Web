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
        private IDoacaoItemRepository _doacaoItemRepository;
        private IDoacaoRepository _doacaoRepository;
        private IOngRepository _ongRepository;

        public DoacaoController(IItemRepository itemRepository, IDoacaoRepository doacaorepository, 
            IDoacaoItemRepository doacaoItemRepository, IOngRepository ongRepository)
        {
            _itemRepository = itemRepository;
            _doacaoRepository = doacaorepository;
            _doacaoItemRepository = doacaoItemRepository;
            _ongRepository = ongRepository;
        }

        public IActionResult Index()
        {
            ViewBag.doacoes = _doacaoRepository.Listar();
            IList<DoacaoItem> doacoesItens = _doacaoItemRepository.Listar();
            IList<DoacaoItem> itens = new List<DoacaoItem>();
            IList<Ong> ongs = new List<Ong>();

            foreach (var item in _itemRepository.Listar())
            {
                foreach (var di in doacoesItens)
                {
                    if (item.ItemId == di.ItemId)
                    {
                        itens.Add(di);
                        foreach (var o in _ongRepository.Listar())
                        {
                            if (o.OngId == di.Doacao.CodigoOng)
                            {
                                ongs.Add(o);
                            }
                        }

                    }
                }
            }
            ViewBag.itens = itens;
            ViewBag.ongs = ongs;
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
            if (ModelState.IsValid)
            {
                doacaoItem.ItemId = id;

                IList<DoacaoItem> lista = HttpContext.Session
                    .GetObjectFromJson<List<DoacaoItem>>("ListaDoacao");
                lista.Add(doacaoItem);
                HttpContext.Session.SetObjectAsJson("ListaDoacao", lista);
                return RedirectToAction("Cadastrar");
            }
            TempData["Erro"] = "Veja se você colocou a quantidade e data de validade do item!";
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
