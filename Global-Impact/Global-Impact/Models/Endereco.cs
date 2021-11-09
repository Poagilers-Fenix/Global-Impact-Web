using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Impact.Models
{
    [Table("Tb_Endereco")]
    public class Endereco
    {
        public int EnderecoId { get; set; }

        [Display(Name = "CEP")]
        [Required]
        public string Cep { get; set; }

        [MinLength(4, ErrorMessage = "O nome do bairro precisa ter mais de 4 caracteres.")]
        [Required]
        public string Bairro { get; set; }

        [MinLength(4, ErrorMessage = "O nome da cidade precisa ter mais de 4 caracteres.")]
        [Required]
        public string Cidade { get; set; }

        [Required]
        public string UF { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "O nome do Logradouro deve ter 5 caracteres ou mais.")]
        [MaxLength(40, ErrorMessage = "O nome do Logradouro deve ter 40 caracteres ou menos.")]
        public string Logradouro { get; set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "O número não pode ficar vazio.")]
        [MinLength(2, ErrorMessage = "O número deve ter, no mínimo 2, caracteres.")]
        public string Numero { get; set; }

        public string Complemento { get; set; }

    }
}
