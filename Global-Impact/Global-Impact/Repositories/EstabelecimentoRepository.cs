using Global_Impact.Models;
using Global_Impact.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Impact.Repositories
{
    public class EstabelecimentoRepository : IEstabelecimentoRepository
    {

        private WefeedContext _context;

        public EstabelecimentoRepository(WefeedContext context)
        {
            _context = context;
        }

        public Estabelecimento BuscarPorId(int id)
        {
            return _context.Estabelecimentos.Where(e => e.EstabelecimentoId == id)
                .Include(e => e.Endereco).FirstOrDefault();
        }

        public void Cadastrar(Estabelecimento estabelecimento)
        {
            _context.Estabelecimentos.Add(estabelecimento);
        }

        public void Editar(Estabelecimento estabelecimento)
        {
            _context.Estabelecimentos.Update(estabelecimento);
        }

        public void Remover(int id)
        {
            _context.Estabelecimentos.Remove(_context.Estabelecimentos.Find(id));
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }
    }
}
