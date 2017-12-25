using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DivisionWebGlobal.Models.Data;

namespace DivisionWebGlobal.DAL
{
    public class MainDbContext : DbContext 
    {
        public MainDbContext() : base("name = MainDatabaseConnection") { }

        public DbSet<Config> DvHeads { get; set; }

        public DbSet<Event> DvHeadEvents { get; set; }
        
        public DbSet<HeadState> DvHeadStates { get; set; }

        public DbSet<HeadTable> DvHeadTables { get; set; }

        public DbSet<SecurityObject> SecurityObjects { get; set; }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<Address> Addresses { get; set; }
    }
}
