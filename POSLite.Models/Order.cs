using POSLite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POSLite.Domain
{
   public class Order:Base
    {
        public Order()
        {
            
            CreatedAt = IDateTime.Now();
            UpdatedAt = IDateTime.Now();
            Terminus = Environment.MachineName;
            OrderDate= IDateTime.Now(); ;
            //  CreatedBy = null;
            // UpdatedBy = null;
        }
        [Display(AutoGenerateField = false)]
        [Key]
        public Guid OrderId { get; set; }
        public string OrderNo { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer{ get; set; }
        public DateTime OrderDate { get; set; }
        public Guid SalesOutletId { get; set; }
        public SalesOutlet SalesOutlet { get; set; }
        public Guid StaffId { get; set; }
        public Staff Staff { get; set; }
        public float VAT { get; set; }
        public float Discount { get; set; }
        public float OrderAmount { get; set; }
        public float TotalAmount { get; set; }
       
    }
}
