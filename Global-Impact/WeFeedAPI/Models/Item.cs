using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WeFeedAPI.Models
{
    [Table("Tb_Item")]
    public class Item
    {
        public int ItemId { get; set; }

        [Required(ErrorMessage = "O nome do item não pode ficar vazio."),
            MinLength(3, ErrorMessage = "O nome deve ter, no mínimo, 3 caracteres."), 
            MaxLength(50, ErrorMessage = "O nome deve ter, no máximo, 40 caracteres.")]
        public string Nome { get; set; }

        public string Foto { get; set; }

        public IList<DoacaoItem> DoacoesItens { get; set; }

    }
}
