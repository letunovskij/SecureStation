using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DivisionWebGlobal.Models.Data;
using DivisionWebGlobal.Models.Structure;

namespace DivisionWebGlobal.Models.Dto
{
    public class TablesDto
    {
        public Config DvHead { get; set; }

        public IEnumerable<ConfigurationDevice> ConfigurationTable { get; set; }

        public IEnumerable<ExternalDevice> ExternalTable { get; set; }

        public Address Address { get; set; }

        public bool Failure { get; set; }
    }
}