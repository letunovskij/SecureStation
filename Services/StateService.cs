using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DivisionWebGlobal.Models.Data;
using DivisionWebGlobal.DAL;

namespace DivisionWebGlobal.Services
{
    public class StateService
    {
        internal HeadState RequestLastStateById(int idHead)
        {
            using (MainDbContext dbContext = new MainDbContext())
            {
                HeadState hd = dbContext.DvHeadStates.Where(t => t.Idhead == idHead).OrderByDescending(t => t.Time).First();
                return hd;
            }
        }
    }
}