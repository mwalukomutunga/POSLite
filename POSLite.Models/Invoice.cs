using POSLite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POSLite.Domain
{
   public class Invoice :Base
    {
        public Invoice()
        {
            CreatedAt = IDateTime.Now();
            UpdatedAt = IDateTime.Now();
            Terminus = Environment.MachineName;
            InvoiceDate = IDateTime.Now();
            //  CreatedBy = null;
            // UpdatedBy = null; 
        }
        [Key]
        public Guid InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime InvoiceDate { get; set; }
        public Guid SalesOutletId { get; set; }
        public SalesOutlet SalesOutlet { get; set; }
        public Guid StaffId { get; set; }
        public Staff Staff { get; set; }
        public float VAT { get; set; }
        public float Discount { get; set; }
        public float InvoiceAmount { get; set; }
        public float TotalAmount { get; set; }
    }
}
