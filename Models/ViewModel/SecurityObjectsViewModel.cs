using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DivisionWebGlobal.Models.Data
{
    // таблица security_object - список всех объектов
    public class SecurityObjectsViewModel
    {
        // список городов: название города, список микрорайонов, количество объектов в городе разных типов
        public IEnumerable<CityViewModel> Cities { get; set; }

        // тревожные сообщения
        public IEnumerable<Event> Events { get; set; }

        // состояние охраняемых объектов
    }
}