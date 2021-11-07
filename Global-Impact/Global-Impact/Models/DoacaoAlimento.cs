using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Impact.Models
{
    [Table("Tb_DoacaoAlimento")]
    public class DoacaoAlimento
    {
        public int DoacaoId { get; set; }
        public Doacao Doacao { get; set; }
        public int AlimentoId { get; set; }
        public Alimento Alimento { get; set; }
        public DateTime DataValidade { get; set; }
        public string Quantidade { get; set; }
        public string Foto { get; set; }
        public Estado Estado { get; set; }
    }
    public enum Estado
    {
        Crítico, Intermediário, Sussa
    }
}
