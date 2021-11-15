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
            if (ModelState.IsValid)
            {
                string senha = Request.Form["senha"];
                string confirmaSenha = Request.Form["confirmaSenha"];

                if (senha == confirmaSenha)
                {
                    _estabRepository.Cadastrar(estab);
                    _estabRepository.Salvar();
                    HttpContext.Session.SetObjectAsJson("EstabSessao", estab);
                    TempData["msg"] = "Estabelecimento cadastrado com sucesso!";
                    return RedirectToAction("Index", "home");
                }
                TempData["Erro"] = "As senhas são diferentes!";
            }
            return View();
        }

        [HttpGet]
        public IActionResult Editar()
        {
            Estabelecimento estabSessao = HttpContext.Session.GetObjectFromJson<Estabelecimento>("EstabSessao");
            return View(estabSessao);
        }

        [HttpPost]
        public IActionResult Editar(Estabelecimento estab)
        {
            if (ModelState.IsValid)
            {
                string senha = Request.Form["senha"];
                string confirmaSenha = Request.Form["confirmaSenha"];
                Estabelecimento estabSessao = HttpContext.Session.GetObjectFromJson<Estabelecimento>("EstabSessao");
                if (senha == estabSessao.Senha)
                {
                    if (senha == confirmaSenha)
                    {
                        _estabRepository.Editar(estab);
                        _estabRepository.Salvar();
                        HttpContext.Session.SetObjectAsJson("EstabSessao", estab);
                        TempData["Sucesso"] = "Dados Atualizados com sucesso!";
                    } else { TempData["Erro"] = "As senhas informadas são diferentes!"; }
                } else { TempData["Erro"] = "A senha informada não é a senha atual."; }

            }
            return View();
        }

        [HttpGet]
        public IActionResult EditarSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditarSenha(string senha)
        {
            string novaSenha = Request.Form["novaSenha"];
            string confirma = Request.Form["confirma"];
            Estabelecimento estab = HttpContext.Session.GetObjectFromJson<Estabelecimento>("EstabSessao");

            if (estab.Senha == senha)
            {
                if(novaSenha == confirma)
                {
                    estab.Senha = novaSenha;
                    HttpContext.Session.SetObjectAsJson("EstabSessao", estab);
                    TempData["Sucesso"] = "Senha alterada com sucesso!";
                } else { TempData["Erro"] = "As senhas informadas no campo de 'Nova Senha' e de confirmação são diferentes!"; }
            } else { TempData["Erro"] = "A senha informada não é a senha atual."; }
            return View();
        }

        [HttpPost]
        public IActionResult Apagar(string senha)
        {
            Estabelecimento estabSessao = HttpContext.Session.GetObjectFromJson<Estabelecimento>("EstabSessao");

            if (senha == estabSessao.Senha)
            {
                _estabRepository.Remover(estabSessao.EstabelecimentoId);
                _estabRepository.Salvar();
                HttpContext.Session.Clear();
            }
            return RedirectToAction("Index", "home");
        }

    }
}
