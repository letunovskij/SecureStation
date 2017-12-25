using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DivisionWebGlobal.Models.Data
{
    /// <summary>
    /// Значимые события системы автоматизации DIVISION
    /// </summary>
    [Table("event")]
    public class Event
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Display(Name = "Время")]
        [Column("time")]
        public DateTime Time { get; set; }

        [Display(Name = "ID устройства автоматизации")]
        [Column("idhead")]
        public int Idhead { get; set; }

        [Display(Name = "IP адрес")]
        [ForeignKey("Idhead")]
        public virtual Config DvHead { get; set; }

        [Display(Name = "Класс события")]
        [Column("event_type")]
        public String EventType { get; set; }

        [Display(Name = "Раздел")]
        [Column("event")]
        public String EventOriginal { get; set; }

        [Display(Name = "Описание события")]
        [Column("event_decrypt")]
        public String EventDecrypt { get; set; }

        [Display(Name = "Тревожное событие")]
        [Column("alarm")]
        public int Alarm { get; set; }

        [Display(Name = "Событие обработано оператором")]
        [Column("process")]
        private int Process { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsProcess
        {
            get {
                return Process > 0;
            }
            set
            {
                Process = value ? 1 : 0; 
            }
        }

        [Display(Name = "Дальнейшие действия оператора")]
        [Column("cnt_activity")]
        public string ContinueActivity { get; set; }
        //TODO Добавить отдельную таблицу для операторов (operator). Добавить отдельную таблицу для обработанных событий (event_history). Возможно добавить таблицу связи       
    }
}