using System;
using System.Collections.Generic;
using System.Text;

namespace POSLite.Domain.Enums
{
    public enum Gender
    {
        Male,
        Female,
        Unknown
    }
    public interface IDateTime
    {
        static DateTime Now() { return DateTime.UtcNow.AddHours(3); }
    }
   
}
