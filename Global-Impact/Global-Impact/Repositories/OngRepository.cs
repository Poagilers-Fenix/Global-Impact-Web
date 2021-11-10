using Global_Impact.Models;
using Global_Impact.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Global_Impact.Repositories
{
    public class OngRepository : IOngRepository
    {
        private WefeedContext _context;

        public OngRepository(WefeedContext context)
        {
            _context = context;
        }

        public IList<Ong> BuscarPor(Expression<Func<Ong, bool>> filtro)
        {
            return _context.ONGs.Where(filtro).Include(o => o.Endereco).ToList();
        }

        public Ong BuscarPorId(int id)
        {
            return _context.ONGs.Where(o => o.OngId == id)
                .Include(o => o.Endereco).FirstOrDefault();
        }

        public void Cadastrar(Ong ong)
        {
            _context.ONGs.Add(ong);
        }

        public void Editar(Ong ong)
        {
            _context.ONGs.Update(ong);
        }

        public IList<Ong> Listar()
        {
            return _context.ONGs.Include(o => o.Endereco).ToList();
        }

        public void Remover(int id)
        {
            _context.ONGs.Remove(_context.ONGs.Find(id));
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }
    }
}
