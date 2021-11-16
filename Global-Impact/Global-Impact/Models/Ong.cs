using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Impact.Models
{
    [Table("Tb_Ong")]
    public class Ong
    {
        public int OngId { get; set; }

        [Display(Name = "Nome da ong")]
        [Required(ErrorMessage = "O nome da ONG não pode ficar vazio.")]
        [MinLength(3, ErrorMessage = "O nome da ONG deve ter, no mínimo, 3 caracteres.")]
        [MaxLength(40, ErrorMessage = "O nome da ONG deve ter, no máximo, 40 caracteres.")]
        public string Nome { get; set; }

        [Display(Name = "Descrição da ong")]
        [Required(ErrorMessage = "Informe a descrição da ONG.")]
        [MinLength(30, ErrorMessage = "A descrição da ONG deve ter, no mínimo, 25 caracteres.")]
        [MaxLength(300, ErrorMessage = "A descrição da ONG deve ter, no máximo, 125 caracteres.")]
        public string Descricao { get; set; }

        // um-para-um endereco
        public Endereco Endereco { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Informe o telefone")]
        [RegularExpression(@"(^[(])([0-9]{2})([)])( {1})([9]{1})?([0-9]{3,4})([-])([0-9]{4})", ErrorMessage = "O telefone deve seguir o seguinte formato:  (11) 91234-5678, ou (11) 1234-5678")]
        [MinLength(10)]
        [MaxLength(15)]
        public string Telefone { get; set; }

        [MaxLength(1000)]
        public string Foto { get; set; }


        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Informe a senha.")]
        [MinLength(6, ErrorMessage = "A senha da ONG deve ter, no mínimo, 6 caracteres.")]
        [MaxLength(25)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Informe o E-mail.")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(30)]
        public string Email { get; set; }

    }
}
