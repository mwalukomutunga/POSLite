using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using POSLite.Persistance;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace POSLite.Client
{
  public static class Config
    {
   
        public static DbContextOptionsBuilder<AppDataContext> LoadOptions()
        {
            DbContextOptionsBuilder<AppDataContext> optionsBuilder = new DbContextOptionsBuilder<AppDataContext>();
            optionsBuilder.UseSqlite(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            return optionsBuilder;
        }

        public static string GetString()
        {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
    }
}
