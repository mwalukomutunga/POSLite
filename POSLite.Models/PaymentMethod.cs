using DevExpress.Mvvm.DataAnnotations;
using POSLite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POSLite.Domain
{
   public class PaymentMethod:Base
    {
        public PaymentMethod()
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
        [Display(GroupName = "<1>", Name = "Method Code")]
        public string PaymentMethodCode { get; set; }
        [Display(GroupName = "<1>", Name = "Method Name")]
        public string PaymentMethodName { get; set; }
        [Display(GroupName = "<2>", Name = "Method Description")]
        public string PaymentMethodDescription { get; set; }
        [LayoutControlEditor(TemplateKey = "IconTemplate")]
        [Display(GroupName = "<2>", Name = "Icon")]
        public byte[] Icon { get; set; }
    }
}
