using POSLite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POSLite.Domain
{
   public class Brand: Base
    {
        public Brand()
        {
           
            CreatedAt = IDateTime.Now();
            UpdatedAt = IDateTime.Now();
            Terminus = Environment.MachineName;
            //  CreatedBy = null;
            // UpdatedBy = null;
        }

        [Display(AutoGenerateField = false)]
        [Key]
        public Guid BrandId { get; set; }
        [Display(GroupName = "<Brand>", Name = "Name")]
        public string Name { get; set; }
    }
}
