using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Global_Impact.Models
{
    [Table("Tb_Doacao")]
    public class Doacao
    {
        public int DoacaoId { get; set; }

        // muitos-para-um estabelecimento
        [HiddenInput]
        public int CodigoEstab { get; set; }

        // muitos-para-um ong
        [HiddenInput]
        public int CodigoOng { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataDoacao { get; set; }

        // muitos-para-muitos
        public IList<DoacaoItem> DoacoesAlimentos { get; set; }

    }
}
