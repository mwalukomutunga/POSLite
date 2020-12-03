using DevExpress.Mvvm.DataAnnotations;
using POSLite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace POSLite.Domain
{
   public class Item: Base
    {
        public Item()
        {
            CreatedAt = IDateTime.Now();
            UpdatedAt = IDateTime.Now();
            Terminus = Environment.MachineName;
            VAT = 16;
            ReorderLevel = 5;
            IsActive = true;
            //  CreatedBy = null;
            // UpdatedBy = null;
        }
        [Display(AutoGenerateField = false)]
        [Key]
        public Guid ItemId { get; set; }

       
        [Display(GroupName = "<1>", Name = "Bar Code")]
        public string BarCode { get; set; }

        [Display(GroupName = "<2>", Name = "Category")]
        [LayoutControlEditor(TemplateKey = "lookUpTemplate")]
        public Guid CategoryId { get; set; }
        [Display(AutoGenerateField = false)]
        public ItemCategory Category { get; set; }

        [Display(GroupName = "<1>", Name = "Name")]
        public string Name { get; set; }

        
        //[Display(GroupName = "<2>", Name = "Category")]
        //public Guid CategoryId { get; set; }

        [Display(GroupName = "<2>", Name = "UOM Code")]
        [LayoutControlEditor(TemplateKey = "UOMlookUp")]
        public Guid UOMCodeId { get; set; }
        [Display(AutoGenerateField = false)]
        
        public UnitOfMeasurement UOMCode { get; set; }
        [Display(GroupName = "<3>", Name = "Brand")]
        [LayoutControlEditor(TemplateKey = "BrandlookUp")]
        public Guid BrandId { get; set; }
        [Display(AutoGenerateField = false)]
        public Brand Brand { get; set; }
        [Display(GroupName = "<3>", Name = "Cost Price")]
        public float CostPrice { get; set; }
        [Display(GroupName = "<price>", Name = "Retail Price")]
        public float RetailPrice { get; set; }
        [Display(GroupName = "<price>", Name = "Discount")]

        public float Discount { get; set; }
        [Display(GroupName = "<4>", Name = "VAT (%)")]
        public int VAT { get; set; }

       
        [Display(GroupName = "<4>", Name = "Reorder Level")]
        public int ReorderLevel { get; set; }
        [Display(GroupName = "<5>", Name = "IsActive")]
        public bool IsActive { get; set; }

        [Display(GroupName = "<5>", Name = "Other Details")]
        public string OtherDetails { get; set; }

    }
}
