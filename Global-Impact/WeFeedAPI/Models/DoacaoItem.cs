using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WeFeedAPI.Models
{
    [Table("Tb_DoacaoAlimento")]
    public class DoacaoItem
    {
        public int DoacaoId { get; set; }

        public Doacao Doacao { get; set; }

        public int ItemId { get; set; }

        public Item Item { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "A data de validade não pode ficar vazia.")]
        [Display(Name = "Data de Validade")]
        public DateTime DataValidade { get; set; }

        [Required(ErrorMessage = "A quantidade não pode ficar vazia.")]
        [Column(TypeName = "decimal(5,2)")] // máx 999,99
        public decimal Quantidade { get; set; }

        [Required(ErrorMessage = "A unidade de medida não pode ficar vazia")]
        public UnidadeMedida UnidadeMedida { get; set; }

        public string Foto { get; set; }

    }
    public enum UnidadeMedida
    {
        un, Kg, L
    }
}
