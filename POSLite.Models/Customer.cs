using POSLite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POSLite.Domain
{
    public class Customer: Base
    {
        public Customer()
        {
           
            CreatedAt = IDateTime.Now();
            UpdatedAt = IDateTime.Now();
            DateOfBirth = IDateTime.Now().AddYears(-20);
            Terminus = Environment.MachineName;
            //  CreatedBy = null;
            // UpdatedBy = null;
        }
        
        [Display(AutoGenerateField = false)]
        [Key]
        public Guid CustomerId{ get; set; }
        [Display(GroupName = "<1>", Name = "Name")]
        public string FullName { get; set; }
        [Display(GroupName = "<1>", Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Display(GroupName = "<2>", Name = "Gender")]
        public Gender Gender  { get; set; }
        [Display(GroupName = "<4>", Name = "Address")]
        public string Address { get; set; }
        [Display(GroupName = "<4>", Name = "Account Balance")]
        public decimal CurrentBalance { get; set; }
        [Display(GroupName = "<5>", Name = "Date Of Last Deposit")]
        public DateTime DateOfLastDeposit { get; set; }
        [Display(GroupName = "<5>", Name = "Amount Of Last Deposit")]
        public float AmountOfLastDeposit { get; set; }

        [Display(GroupName = "<6>", Name = "Remarks")]
        public string  OtherDetails { get; set; }
    }
}
