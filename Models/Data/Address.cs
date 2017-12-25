using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DivisionWebGlobal.Models.Data
{
    [Table("address")]
    public class Address
    {
        [Display(Name = "ID")]
        [Key]
        [Column("idaddress")]
        public int IdAddress { get; set; }

        [Display(Name = "Субъект РФ")]
        [Column("ter_entity")]
        public string TerritorialEntity { get; set; }

        [Display(Name = "Город")]
        [Column("city")]
        [Required]
        public string City { get; set; }

        [Display(Name = "Микрорайон")]
        [Column("mkrn")]
        [Required]
        public string Mkrn { get; set; }

        [Display(Name = "Населенный пункт")]
        [Column("hamlet")]
        public string Hamlet { get; set; }

        [Display(Name = "Улица")]
        [Column("street")]
        //[Required]
        public string Street { get; set; }

        [Display(Name = "Дом")]
        [Column("house")]
        public string House { get; set; }

        [Display(Name = "Квартира")]
        [Column("flat")]
        public string Flat { get; set; }

        [Display(Name = "Широта")]
        [Column("lat")]
        [Required]
        public string Latitude { get; set; }

        [Display(Name = "Долгота")]
        [Column("lon")]
        [Required]
        public string Longitude { get; set; }

        public virtual ICollection<Config> Configs { get; set; }
    }
}