using POSLite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POSLite.Domain
{
  public  class InvoiceLineItem:Base
    {
        public InvoiceLineItem()
        {
            CreatedAt = IDateTime.Now();
            UpdatedAt = IDateTime.Now();
            Terminus = Environment.MachineName;
            //  CreatedBy = null;
            // UpdatedBy = null;
        }
        [Key]
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public Item Item { get; set; }

        public Guid InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public string Name { get; set; }
        public float Qty { get; set; }
        public float Price { get; set; }
        public float VAT { get; set; }
        public float Discount { get; set; }
        public float Total { get; set; }
    }
}
