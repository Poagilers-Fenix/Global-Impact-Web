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

        [Required(ErrorMessage = "O nome do estabelecimento não pode ficar vazio.")]
        [MinLength(3, ErrorMessage = "O nome do estabelecimento deve ter, no mínimo, 3 caracteres.")]
        public string Nome { get; set; }

        [Display(Name = "CNPJ")]
        [MinLength(14, ErrorMessage = "O CNPJ deve ter 14 caracteres.")]
        [Required(ErrorMessage = "O CNPJ não pode ficar vazio.")]
        public string Cnpj { get; set; }

        // um-para-um
        [Required]
        public Endereco Endereco { get; set; }

        [Required(ErrorMessage = "O telefone não pode ficar vazio.")]
        [MinLength(10, ErrorMessage = "O telefone está incompleto. Lembre-se de colocar o DDD.")]
        public string Telefone { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "O e-mail não pode ficar vazio.")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha não pode ficar vazia.")]
        [MinLength(6, ErrorMessage = "A senha deve ter, no mínimo, 6 caracteres.")]
        public string Senha { get; set; }

        // um-para muitos doacao
        public IList<Doacao> Doacoes { get; set; }

    }
}
