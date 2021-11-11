using Global_Impact.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Impact.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Item item)
        {
            TempData["Sucesso"] = $"Item '{item.Nome}' cadastrado!";
            return View();
        }
    }
}
