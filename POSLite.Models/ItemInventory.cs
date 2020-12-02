using DevExpress.Mvvm.DataAnnotations;
using POSLite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POSLite.Domain
{
    public class ItemInventory : Base
    {
        private float quantity;

        public ItemInventory()
        {
            CreatedAt = IDateTime.Now();
            UpdatedAt = IDateTime.Now();
            Terminus = Environment.MachineName;
            InventoryDate = IDateTime.Now();
            //  CreatedBy = null;
            // UpdatedBy = null;
        }
        [Display(AutoGenerateField = false)]
        [Key]
        public Guid InventoryId { get; set; }

        [Display(GroupName = "<1>", Name = "Item")]
        [LayoutControlEditor(TemplateKey = "ItemlookUp")]
        public Guid ItemId { get; set; }
        [Display(AutoGenerateField = false)]
        public Item Item { get; set; }
        [Display(AutoGenerateField = false)]
        public DateTime InventoryDate { get; set; }
        [Display(GroupName = "<2>", Name = "Initial Stock")]
        public float Quantity { get => quantity; set { quantity = value;} }
        [Display(AutoGenerateField = false)]
        public float QuantityInStock { get; set; } = 0;
        [Display(AutoGenerateField = false)]
        public string Reference { get; set; }
        [Display(GroupName = "<1>", Order =0, Name = "Cost Center")]
        [LayoutControlEditor(TemplateKey = "CostCenterlookUp")]
        public Guid CostCenterId { get; set; }
        [Display(AutoGenerateField = false)]
        public SalesOutlet CostCenter { get; set; }

        [Display(GroupName = "<2>", Name = "Other Details")]
        public string OtherDetails { get; set; }
    }
}
