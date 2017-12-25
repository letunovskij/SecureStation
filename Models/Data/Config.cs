using DivisionWebGlobal.Models.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivisionWebGlobal.Models.Data
{
    /// <summary>
    /// Конфигурации систем автоматизации DV-HEAD OMEGA
    /// </summary>
    [Table("config")]
    public class Config
    {
        [Display(Name = "ID")]
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Display(Name = "Рассылку на email осуществлять")]
        [Column("send")]
        public int NeedToSend { get; set; } // TODO вернуть обратно

        // Для отображения поля NeedToSend в виде чекбокса
        [Display(Name = "Рассылку на email осуществлять")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsNeedToSend {
            get {
                return NeedToSend > 0;
            }
            set {
                NeedToSend = value ? 1 : 0;
            }
        }

        // Для отображения поля NeedToSend в кастомизированном для заказчика компоненте. Можно перенести в контроллер
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public CheckBoxListItem IsNeedToSendDisplay { get ; set; }

        [Display(Name = "Владелец")]
        [Column("owner_name")]
        public String OwnerName { get; set; }

        [Display(Name = "Адрес")]
        [Column("place_address")]
        public String PlaceAddress { get; set; }

        [Display(Name = "IP адрес")]
        [Column("ipaddress")]
        public String Ipaddress { get; set; }

        [Display(Name = "Порт")]
        [Column("port")]
        public int Port { get; set; }

        [Display(Name = "Пароль")]
        [Column("password")]
        public String Password { get; set; }

        [Display(Name = "Серийник")]
        [Column("serial_number")]
        public String SerialNumber { get; set; }

        [Display(Name = "Комментарий")]
        [Column("comment")]
        public String Comment { get; set; }

        [Display(Name = "Тревожный")]
        [Column("alarm")]
        public int Alarm { get; set; }

        [Display(Name = "Поставлен под охрану")]
        [Column("under_security")]
        public int UnderSecurity { get; set; }

        [Column("idplace")]
        public int IdPlace { get; set; }

        [ForeignKey("IdPlace")]
        public virtual Address Address { get; set; }

        [Column("idowner")]
        public int IdOwner { get; set; }

        [ForeignKey("IdOwner")]
        public virtual Owner Owner { get; set; }

        // фио и ip-адрес
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Display(Name = "ФИО владельца и IP-адрес")]
        public string FioIp
        {
            get
            {
                return this.OwnerName + ", " + this.Ipaddress;
            }
        }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<HeadState> HeadStates { get; set; }
        
        public virtual ICollection<HeadTable> HeadTables { get; set; } 
    }
}
