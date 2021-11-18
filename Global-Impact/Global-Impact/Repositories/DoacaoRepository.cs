using Global_Impact.Models;
using Global_Impact.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Impact.Repositories
{
    public class DoacaoRepository : IDoacaoRepository
    {
        private WefeedContext _context;

        public DoacaoRepository(WefeedContext context)
        {
            _context = context;
        }

        public void Cadastrar(Doacao doacao)
        {
            _context.Doacoes.Add(doacao);
        }

        public IList<Doacao> Listar()
        {
            return _context.Doacoes.ToList();
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }
    }
}
