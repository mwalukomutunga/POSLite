using POSLite.Domain;
using POSLite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POSLite.Persistance
{
   public  class Seed
    {
        public static void SeedData(AppDataContext context)
        {
            if (context.Customers.Any())
            {
                var customer = new Customer {
                    CustomerId = Guid.NewGuid(),
                    FullName = "Walkin Customer",
                    Gender = Domain.Enums.Gender.Unknown,
                    AmountOfLastDeposit = 0,
                    CurrentBalance = 0,
                    DateOfBirth = IDateTime.Now().AddYears(-50),
                    CreatedAt = IDateTime.Now(),
                    DateOfLastDeposit = IDateTime.Now(),
                    OtherDetails ="Anonymous customer",
                    Terminus =Environment.MachineName,
                     UpdatedAt = IDateTime.Now(),
                };
                context.Customers.Add(customer);
            }

        }
    }
}
