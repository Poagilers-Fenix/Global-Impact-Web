using Global_Impact.Models;
using Global_Impact.Persistence;
using Global_Impact.Repositories;
using Global_Impact.SessionHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Global_Impact.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private WefeedContext _context;
        private IEstabelecimentoRepository _estabRepository;

        public HomeController(ILogger<HomeController> logger, WefeedContext context, IEstabelecimentoRepository estabRepository)
        {
            _logger = logger;
            _context = context;
            _estabRepository = estabRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Estabelecimento estab)
        {
            Estabelecimento estabEncontrado = _estabRepository.BuscarPor(e => e.Email == estab.Email);
            if (estabEncontrado != null)
            {
                if (estabEncontrado.Senha == estab.Senha)
                {
                    HttpContext.Session.SetObjectAsJson("EstabSessao", estabEncontrado);
                    return RedirectToAction("index");
                }
            }
            TempData["Erro"] = "E-mail ou senha incorretos!";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index");
        }

        public async Task<IActionResult> BuscarCEP()
        {
            string cep = Request.Form["cep"]; string url = Request.Form["url"];

            HttpClient client = new HttpClient();
            string api = $"http://viacep.com.br/ws/{cep}/json/";
            var response = await client.GetStringAsync(api);
            JObject json = JObject.Parse(response);

            Endereco endereco = new Endereco()
            {
                Cidade = (string) json["localidade"],
                Bairro = (string) json["bairro"],
                Logradouro = (string) json["logradouro"],
                UF = (string) json["uf"],
                Cep = (string)json["cep"],
            };

            TempData["bairro"] = endereco.Bairro; TempData["cidade"] = endereco.Cidade;
            TempData["cep"] = endereco.Cep; TempData["logradouro"] = endereco.Logradouro;
            TempData["uf"] = endereco.UF;
            return Redirect(url);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
