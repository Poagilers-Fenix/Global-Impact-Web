using Global_Impact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Global_Impact.Repositories
{
    public interface IOngRepository
    {
        void Cadastrar(Ong ong);
        void Editar(Ong ong);
        IList<Ong> Listar();
        void Remover(int id);
        Ong BuscarPorId(int id);
        IList<Ong> BuscarPor(Expression<Func<Ong, bool>> filtro);
        void Salvar();

    }
}
