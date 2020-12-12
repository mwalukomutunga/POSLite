using POSLite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace POSLite.Domain
{
    public class OrderItem : Base, INotifyPropertyChanged
    {
        public OrderItem()
        {
            CreatedAt = IDateTime.Now();
            UpdatedAt = IDateTime.Now();
            Terminus = Environment.MachineName;
            //  CreatedBy = null;
            // UpdatedBy = null;
        }
        private float qty;
        private float price;
        private float discount;
        private float _Vat;
        [Display(AutoGenerateField = false)]
        [Key]
        public Guid Id { get; set; }
        [Display(AutoGenerateField = false)]
        public Guid OrderId { get; set; }
        [Display(AutoGenerateField = false)]
        public Order Order { get; set; }

        public string Description { get; set; }
        [Display(AutoGenerateField = false)]
        public Guid ItemId { get; set; }
        [Display(AutoGenerateField = false)]
        public Item Item { get; set; }
        public float Qty { get => qty; set { qty = value; HandleItemChange(); NotifyPropertyChanged(); } }
        public float Discount { get => discount; set { discount = value; HandleItemChange(); NotifyPropertyChanged(); } }
        public float Price { get => price; set { price = value; HandleItemChange(); NotifyPropertyChanged(); } }
        [Display(AutoGenerateField = false)]
        public float VAT { get => (_Vat * Price) / (100 + _Vat); set { _Vat = value; HandleItemChange(); NotifyPropertyChanged(); } }

        public float Total
        {
            get
            {
                return (Qty * Price) - discount;
            }

        }

        private void HandleItemChange()
        {
            NotifyPropertyChanged(nameof(Total));
            NotifyPropertyChanged(nameof(VAT));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
