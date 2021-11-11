using Global_Impact.Models;
using Global_Impact.Persistence;
using Global_Impact.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Impact.Controllers
{
    public class ItemController : Controller
    {
        private IItemRepository _itemRepository;

        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Item item)
        {
            _itemRepository.Cadastrar(item);
            _itemRepository.Salvar();
            TempData["Sucesso"] = $"Item '{item.Nome}' cadastrado!";
            return RedirectToAction("Cadastrar");
        }
    }
}
