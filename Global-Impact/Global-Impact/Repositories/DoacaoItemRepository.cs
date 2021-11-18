using Global_Impact.Models;
using Global_Impact.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Impact.Repositories
{
    public class DoacaoItemRepository : IDoacaoItemRepository
    {
        private WefeedContext _context;

        public DoacaoItemRepository(WefeedContext context)
        {
            _context = context;
        }

        public void Cadastrar(DoacaoItem doacaoItem)
        {
            _context.DoacoesItens.Add(doacaoItem);
        }

        public IList<DoacaoItem> Listar()
        {
            return _context.DoacoesItens.ToList();
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }
    }
}
