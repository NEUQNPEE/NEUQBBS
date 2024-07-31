using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApiDemo.Entities.Factorys
{
    public class DbContextFactory
    {
        public static WebApiDemoContext GetDbContext()
        {
            var dbContext = new WebApiDemoContext();
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return dbContext;
        }
    }
}