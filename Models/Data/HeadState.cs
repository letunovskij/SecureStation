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
    /// История состояний DV-HEAD OMEGA
    /// </summary>
    [Table("head_state")]
    public class HeadState
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Display(Name = "Время запроса к DV-HEAD OMEGA")]
        [Column("time")]
        public DateTime Time { get; set; }

        [Column("idhead")]
        public int Idhead { get; set; }

        [Display(Name = "Напряжение на DV-HEAD OMEGA")]
        [Column("voltage")]
        public String Voltage { get; set; }

        [Display(Name = "Температура на DV-HEAD OMEGA")]
        [Column("temperature")]
        public String Temperature { get; set; }

        [Display(Name = "Состояние DV-HEAD OMEGA")]
        [Column("status")]
        public bool Status { get; set; }

        [Display(Name = "Время на DV-HEAD OMEGA")]
        [Column("head_time")]
        public DateTime HeadTime { get; set; }

        [ForeignKey("Idhead")]
        public virtual Config DvHead { get; set; }
    }
}