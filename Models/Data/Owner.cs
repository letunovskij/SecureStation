using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DivisionWebGlobal.Models.Data
{
    [Table("owner")]
    public class Owner
    {
        [Key]
        [Column("idowner")]
        public int IdOwner { get; set; }

        [Display(Name = "ФИО")]
        [Column("fio")]
        [StringLength(45)]
        public string Fio { get; set; }

        [Display(Name = "Эл. почта")]
        [Column("email")]
        [StringLength(45)]
        public string Email { get; set; }

        [Column("comment")]
        [StringLength(150)]
        public string Comment { get; set; }

        [Column("email2")]
        [StringLength(2000)]
        public string Email2 { get; set; }

        public virtual ICollection<Config> Configs { get; set; }
    }
}