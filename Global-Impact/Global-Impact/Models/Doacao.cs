using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Impact.Models
{
    public class Doacao
    {
        public int DoacaoId { get; set; }
        public Estabelecimento Estabelecimento { get; set; }
        public DateTime DataDoacao { get; set; }
        public Ong Ong { get; set; }
    }
}
