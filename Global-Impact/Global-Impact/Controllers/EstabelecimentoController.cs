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
                IList<Estabelecimento> lista = _estabRepository.Listar();
                if (lista.Count > 0)
                {
                    foreach (var e in lista)
                    {
                        if (e.Cnpj == estab.Cnpj) {
                            TempData["Erro"] = "Um estabelecimento com este CNPJ já foi cadastrado.";
                            return View(); }
                        else if (e.Nome == estab.Nome) {
                            TempData["Erro"] = "Um estabelecimento com este nome já foi cadastrado.";
                            return View(); }
                        else if (e.Email == estab.Email) {
                            TempData["Erro"] = "Um estabelecimento com este e-mail já foi cadastrado.";
                            return View(); }

                        string senha = Request.Form["senha"];
                        string confirmaSenha = Request.Form["confirmaSenha"];

                        if (senha == confirmaSenha) { Cadastro(estab); return RedirectToAction("Index", "Home"); }
                        TempData["Erro"] = "As senhas são diferentes!";
                    }
                }
                else { Cadastro(estab); return RedirectToAction("Index", "Home"); }
            }
            return View();
        }

        public void Cadastro(Estabelecimento estab)
        {
            _estabRepository.Cadastrar(estab);
            _estabRepository.Salvar();
            HttpContext.Session.SetObjectAsJson("EstabSessao", estab);
            TempData["msg"] = "Estabelecimento cadastrado com sucesso!";
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
                    }
                    else { TempData["Erro"] = "As senhas informadas são diferentes!"; }
                }
                else { TempData["Erro"] = "A senha informada não é a senha atual."; }

            }
            return View();
        }

        [HttpGet]
        public IActionResult EditarSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditarSenha(Estabelecimento estab)
        {

            string novaSenha = Request.Form["novaSenha"];
            string confirma = Request.Form["confirma"];
            Estabelecimento estabSessao = HttpContext.Session.GetObjectFromJson<Estabelecimento>("EstabSessao");

            if (estabSessao.Senha == estab.Senha)
            {
                if (novaSenha.Length > 6 && confirma.Length > 6)
                {
                    if (novaSenha == confirma)
                    {
                        estabSessao.Senha = novaSenha;
                        _estabRepository.Editar(estabSessao);
                        _estabRepository.Salvar();
                        HttpContext.Session.SetObjectAsJson("EstabSessao", estabSessao);
                        TempData["Sucesso"] = "Senha alterada com sucesso!";
                    }
                    else { TempData["Erro"] = "As senhas informadas no campo de 'Nova Senha' e de confirmação são diferentes!"; }
                }
                else { TempData["Erro"] = "Os campos de nova senha e de confirmação de nova senha precisam ter, no mínimo, 6 caracteres."; }
            }
            else { TempData["Erro"] = "A senha informada não é a senha atual."; }

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
