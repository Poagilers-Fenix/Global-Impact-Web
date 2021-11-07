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
        [Column("cd_doacao")]
        public int DoacaoId { get; set; }
        [Column("cd_estab")]
        [HiddenInput]
        public int CodigoEstab { get; set; }
        [Column("cd_ong")]
        [HiddenInput]
        public int CodigoOng { get; set; }
        [DataType(DataType.Date)]
        [Column("dt_doacao")]
        public DateTime DataDoacao { get; set; }
        public IList<DoacaoAlimento> DoacoesAlimentos { get; set; }
    }
}
