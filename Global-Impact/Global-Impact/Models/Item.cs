using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Impact.Models
{
    [Table("Tb_Item")]
    public class Item
    {
        public int ItemId { get; set; }

        [Display(Name = "Nome do item")]
        [Required(ErrorMessage = "Informe o nome do item")]
        [MinLength(3, ErrorMessage = "O nome deve ter, no mínimo, 3 caracteres.")]
        [MaxLength(40, ErrorMessage = "O nome deve ter, no máximo, 40 caracteres.")]
        public string Nome { get; set; }
        [Display(Name = "Foto")]
        [MaxLength(1000)]
        public string Foto { get; set; }

        public IList<DoacaoItem> DoacoesItens { get; set; }

    }
}
