using POSLite.Domain.Enums;
using System;

namespace POSLite.Domain
{
    public class ItemPriceLog:Base
    {
        public ItemPriceLog()
        {
           
            CreatedAt = IDateTime.Now();
            UpdatedAt = IDateTime.Now();
            Date = IDateTime.Now();
            Terminus = Environment.MachineName;
        }
        public Guid ID { get; set; }
        public Guid ItemId { get; set; }
        public Item Item { get; set; }
       // public SalesOutlet CostCenter { get; set; }
        public float Discount { get; set; }
        public float CostPrice { get; set; }
        public float RetailPrice { get; set; }
        public DateTime Date { get; set; }
    }
}
