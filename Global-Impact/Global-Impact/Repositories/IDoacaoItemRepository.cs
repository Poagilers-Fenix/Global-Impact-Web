using Global_Impact.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Impact.Repositories
{
    public interface IDoacaoItemRepository
    {
        void Cadastrar(DoacaoItem doacao);

        void Salvar();

        IList<DoacaoItem> Listar();

    }
}
