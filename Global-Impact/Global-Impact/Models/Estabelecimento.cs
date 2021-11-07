using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Impact.Models
{
    [Table("Tb_Estabelecimento")]
    public class Estabelecimento
    {
        public int EstabelecimentoId { get; set; }
        public string Nome { get; set; }
        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; }
        public Endereco Endereco { get; set; }
        public string Telefone { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
