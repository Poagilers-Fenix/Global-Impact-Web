using Global_Impact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Impact.Repositories
{
    public interface IEstabelecimentoRepository
    {
        void Cadastrar(Estabelecimento estabelecimento);
        
        void Editar(Estabelecimento estabelecimento);

        Estabelecimento BuscarPorId(int id);

        void Remover(int id);

        void Salvar();
        
    }
}
