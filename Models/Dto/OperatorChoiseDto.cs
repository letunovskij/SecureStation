using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DivisionWebGlobal.Models.Dto
{
    /// <summary>
    /// Действия оператора по обработке тревожных сообщений
    /// </summary>
    public class OperatorChoiseDto
    {
        public string Choise { get; set; }

        public int EventId { get; set; } 

        public string OperatorEmail { get; set; }

        public int HeadId { get; set; }
    }
}