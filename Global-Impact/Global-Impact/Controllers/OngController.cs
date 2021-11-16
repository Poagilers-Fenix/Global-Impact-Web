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

        public IActionResult Index()
        {
            IList<Ong> listaOng = _ongRepository.Listar();
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
            return RedirectToAction("Cadastrar");
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
            TempData["Erro"] = "Erro ao deletar a ong, veja se o código que você inseriu está correto.";
            return RedirectToAction("PaginaOng", new { id = ongId});
        }

    }
}
