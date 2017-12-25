using DivisionWebGlobal.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DivisionWebGlobal.Models.ViewModel
{
    public class SecurityObjectInfoViewModel
    {
        // список объектов под охраной
        public IEnumerable<Config> Objects { get; set; }

        // тревожные сообщения
        public IEnumerable<Event> Events { get; set; }
    }
}