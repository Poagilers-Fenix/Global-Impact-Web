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

        [Required(ErrorMessage = "O nome da ONG não pode ficar vazio.")]
        [MinLength(3, ErrorMessage = "O nome da ONG deve ter no mínimo 3 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição da ONG não pode ficar vazia.")]
        [MinLength(50, ErrorMessage = "A descrição da ONG deve ter no mínimo 50 caracteres.")]
        public string Descricao { get; set; }

        [Required]
        // um-para-um endereco
        public Endereco Endereco { get; set; }

        [Required(ErrorMessage = "O telefone não pode ficar vazio.")]
        [MinLength(10, ErrorMessage = "O telefone está incompleto. Lembre-se de colocar o DDD.")]
        public string Telefone { get; set; }

        public string Foto { get; set; }

    }
}
