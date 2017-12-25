using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DivisionWebGlobal.Models.Data
{
    /// <summary>
    /// Охраняемые объекты
    /// </summary>
    [Table("security_object")]
    public class SecurityObject
    {
        [Key]
        [Column("idsecurity_object")]
        public int Id { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("mkrn")]
        public string Mkrn { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("info")]
        public string Info { get; set; }

        [Column("umbrella")]
        public bool IsUmbrella { get; set; }

        [Column("crash")]
        public bool IsCrash { get; set; }

        [Column("alarm")]
        public bool IsAlarm { get; set; }

        [Column("debt")]
        public bool IsDebt { get; set; }

        [Column("geodata")]
        public string Geodata { get; set; }
    }
}