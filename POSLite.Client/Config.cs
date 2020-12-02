using Microsoft.EntityFrameworkCore;
using POSLite.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace POSLite.Client
{
  public static   class Config
    {
        public static DbContextOptionsBuilder<AppDataContext> LoadOptions()
        {
            DbContextOptionsBuilder<AppDataContext> optionsBuilder = new DbContextOptionsBuilder<AppDataContext>();
            optionsBuilder.UseSqlite(@"Data Source=F:\Projects\POSLite\POSLite.Client\POS.db");
            return optionsBuilder;
        }
    }
}
