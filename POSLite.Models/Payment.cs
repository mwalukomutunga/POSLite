using POSLite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POSLite.Domain
{
   public class Payment:Base
    {
        public Payment()
        {
            CreatedAt = IDateTime.Now();
            UpdatedAt = IDateTime.Now();
            PaymentDate = IDateTime.Now();
            Terminus = Environment.MachineName;
            //  CreatedBy = null;
            // UpdatedBy = null;
        }
        [Display(AutoGenerateField = false)]
        [Key] 
        public Guid PaymentId { get; set; }
        [Display(GroupName = "<1>", Name = "PaymentMethod")]
        public PaymentMethod PaymentMethod { get; set; }
        [Display(GroupName = "<1>", Name = "Transaction")]
        public Order Transaction { get; set; }
        [Display(GroupName = "<2>", Name = "PaymentAmount")]
        public float PaymentAmount { get; set; }
        [Display(GroupName = "<2>", Name = "OtherDetails")]
        public string OtherDetails { get; set; }
        [Display(AutoGenerateField = false)]
        public DateTime PaymentDate { get; set; }
    }
}
