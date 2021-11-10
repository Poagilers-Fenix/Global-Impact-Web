using Global_Impact.Models;
using Global_Impact.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Global_Impact.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private WefeedContext _context;

        public ItemRepository(WefeedContext context)
        {
            _context = context;
        }

        public IList<Item> BuscarPor(Expression<Func<Item, bool>> filtro)
        {
            return _context.Itens.Where(filtro).ToList();
        }

        public Item BuscarPorId(int id)
        {
            return _context.Itens.Find(id);
        }

        public void Cadastrar(Item item)
        {
            _context.Itens.Add(item);
        }

        public IList<Item> Listar()
        {
            return _context.Itens.ToList();
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }
    }
}
