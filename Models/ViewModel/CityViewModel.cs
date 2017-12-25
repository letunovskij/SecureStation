using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DivisionWebGlobal.Models.Data
{
    /// <summary>
    /// Отображаемая информация о городе
    /// </summary>
    public class CityViewModel
    {
        public string Name { get; set; }

        public List<MkrnViewModel> Mkrns { get; set; }

        public int AllCount { get; set; }

        public int UmbrellasCount { get; set; }

        public int AlarmsCount { get; set; }

        public int CrashsCount { get; set; }

        public int DebtsCount { get; set; }
    }
}