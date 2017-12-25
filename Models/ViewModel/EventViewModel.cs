using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DivisionWebGlobal.Models.Structure;
using PagedList;

namespace DivisionWebGlobal.Models.Data
{
    public class EventViewModel
    {
        public IEnumerable<ConfigurationDevice> ConfigurationTable { get; set; }

        public IEnumerable<ExternalDevice> ExternalTable { get; set; }

        public IPagedList<Event> Events { get; set; }

        public HeadState DvHeadState { get; set; }

        public Config DvHead { get; set; }

        public Address Address { get; set; }

        public string Type { get; set; } = "all";
    }
}