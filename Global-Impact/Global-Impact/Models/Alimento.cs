using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Impact.Models
{
    [Table("Tb_Alimento")]
    public class Alimento
    {
        [Column("cd_alimento")]
        public int AlimentoId { get; set; }
        [Required, Column("nm_alimento"),
            MinLength(3, ErrorMessage = "O nome deve ter, no mínimo, 3 caracteres."), 
            MaxLength(50, ErrorMessage = "O nome deve ter, no máximo, 40 caracteres.")]
        public string Nome { get; set; }
        public IList<DoacaoAlimento> DoacoesAlimentos { get; set; }
    }
}
