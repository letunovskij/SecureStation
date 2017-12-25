using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;

namespace DivisionWebGlobal.Models.Data
{
    /// <summary>
    /// Таблицы устройств DV-HEAD OMEGA
    /// </summary>
    [Table("head_table")]
    public class HeadTable
    {
        [Key]
        [Column("idhead_table")]
        public int Id { get; set; }

        [Column("idhead")]
        public int Idhead { get; set; }

        [Column("dgs_time")]
        public DateTime DgsTime { get; set; }

        [Column("table_config")]
        public String ConfigurationTable { get; set; }
        
        [Column("table_connected")]
        public string ConnectedTable { get; set; }

        [ForeignKey("Idhead")]
        public virtual Config DvHead { get; set; }
    }
}