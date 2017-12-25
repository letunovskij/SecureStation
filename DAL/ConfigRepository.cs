using DivisionWebGlobal.Models.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DivisionWebGlobal.DAL
{
    public class ConfigRepository : IConfigRepository, IDisposable
    {
        private MainDbContext dbContext;

        public ConfigRepository(MainDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Config> GetConfigs()
        {
            return dbContext.DvHeads.ToList();
        }

        public Config GetConfigById(int id)
        {
            return dbContext.DvHeads.Find(id);
        }

        public void InsertConfig(Config config)
        {
            dbContext.DvHeads.Add(config);
        }

        public void DeleteConfig(int id)
        {
            Config config = dbContext.DvHeads.Find(id);
            dbContext.DvHeads.Remove(config); // check проверить Null значения
        }

        public void UpdateConfig(Config config)
        {
            dbContext.Entry(config).State = EntityState.Modified;
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}