using DivisionWebGlobal.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DivisionWebGlobal.DAL
{
    // репозиторий для конфигураций DV-HEAD OMEGA (будет использоваться в 2 проектах - Division Monitor Global и DGS)
    public interface IConfigRepository : IDisposable
    {
        IEnumerable<Config> GetConfigs();
        Config GetConfigById(int configId);
        void InsertConfig(Config config);
        void DeleteConfig(int configId);
        void UpdateConfig(Config config);
        void Save();
    }
}