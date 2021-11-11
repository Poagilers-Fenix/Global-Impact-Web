using Global_Impact.Persistence;
using Global_Impact.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Global_Impact.Controllers
{
    public class OngController : Controller
    {
        private WefeedContext _context;

        public OngController(WefeedContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IList<Ong> listaOng = new List<Ong>();

            Endereco enderecoMock = new Endereco();
            enderecoMock.Cep = "03850005";
            enderecoMock.Logradouro = "Rua Dr Gabriel de Resende";
            enderecoMock.Bairro = "Vila Invernada";
            enderecoMock.Cidade = "São Paulo";
            enderecoMock.UF = "SP";
            enderecoMock.Numero = "122";

            Ong ongMock = new Ong();
            ongMock.Nome = "ong animal";
            ongMock.Descricao = "uma ong legal";
            ongMock.Endereco = enderecoMock;
            ongMock.Telefone = "1222337460";
            ongMock.Foto = "https://quizizz.com/media/resource/gs/quizizz-media/quizzes/007aae49-a1f2-4d3b-b75b-ee004690adf3";



            listaOng.Add(ongMock);

            return View(listaOng);
        }

        public IActionResult PaginaOng(Ong ong)
        {
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
            _context.ONGs.Add(ong);
            _context.SaveChanges();
            TempData["Sucesso"] = "Ong cadastrada com sucesso!";
            return View();
        }
    }
}
