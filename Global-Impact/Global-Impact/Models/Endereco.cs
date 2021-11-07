using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Impact.Models
{
    [Table("Tb_endereco")]
    public class Endereco
    {
        public int EnderecoId { get; set; }
        [Display(Name = "CEP")]
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        //[MinLength(5, ErrorMessage = "O nome do Logradouro deve ter 5 caracteres ou mais.")]
        //[MaxLength(40, ErrorMessage = "O nome do Logradouro deve ter 40 caracteres ou menos.")]
        public string Logradouro { get; set; }
        [Display(Name = "Número")]
        public string Numero { get; set; }
        public string Complemento { get; set; }
    }
}
