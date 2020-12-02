 using POSLite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POSLite.Domain
{
   public class UnitOfMeasurement : Base
    {
        public UnitOfMeasurement()
        {
            CreatedAt = IDateTime.Now();
            UpdatedAt = IDateTime.Now();
            Terminus = Environment.MachineName;
            //  CreatedBy = null;
            // UpdatedBy = null;
        }
        [Display(AutoGenerateField = false)]
        [Key]
        public Guid ID { get; set; }
        [Display(GroupName = "<1>", Name = "UOM Code")]
        public string UOMCode { get; set; }
        [Display(GroupName = "<1>", Name = "UOM Description")]
        public string UOMDescription { get; set; }

    }
}
