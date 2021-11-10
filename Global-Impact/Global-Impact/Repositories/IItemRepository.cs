using Global_Impact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Global_Impact.Repositories
{
    public interface IItemRepository
    {
        void Cadastrar(Item item);

        IList<Item> Listar();

        Item BuscarPorId(int id);

        IList<Item> BuscarPor(Expression<Func<Item, bool>> filtro);

        void Salvar();

    }
}
