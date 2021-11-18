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
using Global_Impact.SessionHelpers;
using System.Text;

namespace Global_Impact.Controllers
{
    public class OngController : Controller
    {
        private IOngRepository _ongRepository;
        private IItemRepository _itemRepository;
        private IDoacaoItemRepository _doacaoItemRepository;


        public OngController(IOngRepository ongRepository, IItemRepository itemRepository, IDoacaoItemRepository doacaoItemRepository)
        {
            _ongRepository = ongRepository;
            _itemRepository = itemRepository;
            _doacaoItemRepository = doacaoItemRepository;
        }

        public IActionResult Index(string cidade)
        {
            IList<Ong> listaOng = _ongRepository.BuscarPor(o =>
                o.Endereco.Cidade.ToLower().Contains(cidade) || cidade == null);
            return View(listaOng);
        }

        public IActionResult PaginaOng(int id)
        {
            Ong ong = _ongRepository.BuscarPorId(id);
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
            _ongRepository.Cadastrar(ong);
            _ongRepository.Salvar();
            TempData["Sucesso"] = "Ong cadastrada com sucesso!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EscolherOng()
        {
            return View(_ongRepository.Listar());
        }

        [HttpPost]
        public IActionResult AdicionarDoacao(int id)
        {
            if (ModelState.IsValid)
            {
                List<DoacaoItem> doacaoItem = HttpContext.Session.GetObjectFromJson<List<DoacaoItem>>("ListaDoacao");
                foreach (var di in doacaoItem)
                {
                    Estabelecimento estab = HttpContext.Session.GetObjectFromJson<Estabelecimento>("EstabSessao");
                    Doacao doacao = new Doacao()
                    {
                        CodigoEstab = estab.EstabelecimentoId,
                        CodigoOng = id,
                        DataDoacao = DateTime.Today.Date
                    };
                    di.Doacao = doacao;
                    _doacaoItemRepository.Cadastrar(di);
                    _doacaoItemRepository.Salvar();
                }
                HttpContext.Session.SetObjectAsJson("ListaDoacao", new List<DoacaoItem>());
                TempData["Sucesso"] = "Doação realizada! Um entregador logo chegará para retirar os itens.";
                return RedirectToAction("Cadastrar", "Doacao");
            }
            TempData["Erro"] = "Algo deu errado! Tente novamente mais tarde.";
            return RedirectToAction("EscolherOng");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            if (HttpContext.Session.GetObjectFromJson<Ong>("ONGSessao") != null) 
            {
                return View(HttpContext.Session.GetObjectFromJson<Ong>("ONGSessao"));
            }
            Ong ong = _ongRepository.BuscarPorId(id);
            HttpContext.Session.SetObjectAsJson("ONGSessao", ong);
            return View(ong);
        }

        [HttpPost]
        public IActionResult Editar(Ong ong)
        {
            if (ModelState.IsValid)
            {
                Ong ongBanco = HttpContext.Session.GetObjectFromJson<Ong>("ONGSessao");
                if (ong.Senha == ongBanco.Senha)
                {
                    _ongRepository.Editar(ong);
                    _ongRepository.Salvar();
                    TempData["Sucesso"] = "Dados da ONG alterados com sucesso!";
                    return RedirectToAction("editar");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditarSenha(int id)
        {
            Ong ong = _ongRepository.BuscarPorId(id);
            HttpContext.Session.SetObjectAsJson("ONGSessao", ong);
            return View(ong);
            //return View();
        }

        [HttpPost]
        public IActionResult EditarSenha(Ong ong)
        {
            string novaSenha = Request.Form["novaSenha"];
            string confirma = Request.Form["confirma"];
            Ong ongSessao = HttpContext.Session.GetObjectFromJson<Ong>("ONGSessao");

            if (ongSessao.Senha == ong.Senha)
            {
                if (novaSenha.Length >= 6 && confirma.Length >= 6)
                {
                    if (novaSenha == confirma)
                    {
                        ongSessao.Senha = novaSenha;
                        _ongRepository.Editar(ongSessao);
                        _ongRepository.Salvar();
                        HttpContext.Session.SetObjectAsJson("ONGSessao", ongSessao);
                        TempData["Sucesso"] = "Senha alterada com sucesso!";
                    }
                    else { TempData["Erro"] = "As senhas informadas no campo de 'Nova Senha' e de confirmação são diferentes!"; }
                }
                else { TempData["Erro"] = "Os campos de nova senha e de confirmação de nova senha precisam ter, no mínimo, 6 caracteres."; }
            }
            else { TempData["Erro"] = "A senha informada não é a senha atual."; }

            return View(ong);
        }

        public IActionResult DeletarOng(string senha, int ongId)
        {
            IList<Ong> Ongs = _ongRepository.Listar();
            foreach (var ong in Ongs)
            {
                if (ong.Senha == senha && ong.OngId == ongId)
                {
                    _ongRepository.Remover(ong.OngId);
                    _ongRepository.Salvar();
                    TempData["Sucesso"] = "Ong deletada com sucesso!";
                    return RedirectToAction("Index");
                }
            }
            TempData["Erro"] = "Erro ao deletar a ong, veja se a senha que você inseriu está correta.";
            return RedirectToAction("PaginaOng", new { id = ongId });
        }

    }
}
