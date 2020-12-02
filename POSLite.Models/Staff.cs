using POSLite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POSLite.Domain
{
   public class Staff : Base
    {
        public Staff()
        {
            CreatedAt = IDateTime.Now();
            UpdatedAt = IDateTime.Now();
            Terminus = Environment.MachineName;
            //  CreatedBy = null;
            // UpdatedBy = null;
        }
        [Display(AutoGenerateField = false)]
        [Key]
        public Guid StaffId { get; set; }
        [Display(GroupName = "<1>", Name = "PIN")]
        public long UserName { get; set; }

        [Display(GroupName = "<1>", Name = "Name")]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public SalesOutlet CostCenter { get; set; }
        public string Password { get; set; }
        [Display(AutoGenerateField = false)]
        public string ConfirmPassword { get; set; }
    }
}
