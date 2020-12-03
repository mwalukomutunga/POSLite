using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using POSLite.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSLite.Client.ViewModels
{
    public class SalesViewModel : BaseViewModel
    {
        protected IDialogService DialogService { get { return this.GetService<IDialogService>("DialogService"); } }
        protected IDialogService DialogService1 { get { return this.GetService<IDialogService>("DialogService1"); } }
        protected IDialogService DiscountService { get { return this.GetService<IDialogService>("DiscountService"); } }
        protected IDialogService CustomerService { get { return this.GetService<IDialogService>("CustomerService"); } }
        private ObservableCollection<OrderItem> _Collection;

        public ObservableCollection<OrderItem> ItemCollection
        {
            get { return _Collection; }
            set { SetValue(ref _Collection, value); RaisePropertyChanged(); }
        }
        public SalesViewModel()
        {
            ItemCollection = new ObservableCollection<OrderItem>();
            CustomerData = new ObservableCollection<Customer>(unitOfWork.CustomerRepository.Get());
            try
            {
                Customer = CustomerData.FirstOrDefault();
                CustomerId = Customer.CustomerId;
                OrderNo = unitOfWork.SalesRepository.GenerateOrderNo();
            }
            catch (Exception){}
        }

        public Guid OrderId { get; set; } = Guid.NewGuid();
        public string OrderNo { get; set; }
        public ObservableCollection<Customer> CustomerData { get; set; }
        public Customer Customer { get; set; }
        private string barCodeText1;

        OrderItem _SelectedObject;
        public OrderItem SelectedObject
        {
            get { return _SelectedObject; }
            set
            {
                SetValue(ref _SelectedObject, value);
                RaisePropertiesChanged();
            }
        }
        Guid _CustomerId;
        public Guid CustomerId
        {
            get { return _CustomerId; }
            set
            {
                SetValue(ref _CustomerId, value);
                 LoadCustomer(value).Wait();
                RaisePropertiesChanged();
            }
        }

        private async Task LoadCustomer(Guid value)
        {
            Customer = await unitOfWork.CustomerRepository.GetByID(value);
        }

        public float Total
        {
            get
            {
                return SubTotal +TotalVAT- TotalDiscount;
            }
        }
        public float SubTotal
        {
            get
            {
                return ItemCollection.Sum(x => x.Total)-TotalVAT;
            }
        }
        public float TotalDiscount
        {
            get
            {
                return ItemCollection.Sum(x => x.Discount);
            }
        }
        public float TotalVAT
        {
            get
            {
                return ItemCollection.Sum(x => x.VAT);
            }
        }
        public string barCodeText { get => barCodeText1; set => SetValue(ref barCodeText1, value); }
        [Command]
        public void Scan(string barCode)
        {
            ScanByBarCode(barCode);

            barCodeText = "";
            RaisePropertyChanged();
        }

        private void ScanByBarCode(string barCode)
        {
            var item = unitOfWork.ItemsRepository.GetItemByBarCode(barCode.Trim().ToString());


            if (item != null)
            {
                var SelectedObject = new OrderItem
                {
                    ItemId = item.ItemId,
                    OrderId = OrderId,
                    Description = item.Name,
                    Qty = 1,
                    Discount = item.Discount,
                    Price = item.RetailPrice,
                    VAT = item.VAT
                };
                var found = ItemCollection.FirstOrDefault(x => x.ItemId == SelectedObject.ItemId);
                if (found == null)
                {
                    ItemCollection.Add(SelectedObject);
                }
                else
                {

                    found.Qty += 1;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //  unitOfWork.Dispose();
            }

            disposed = true;
        }

        [Command]
        public void Delete()
        {
            if (SelectedObject != null)
                ItemCollection.Remove(SelectedObject);
        }

        [Command]
        public void AddQty()
        {
            if (SelectedObject!=null) SelectedObject.Qty += 1;
             RaisePropertyChanged();

        }
        [Command]
        public void DeductQty()
        {
            if (SelectedObject != null) SelectedObject.Qty -= 1;
            RaisePropertyChanged();
        }
        [Command]
        public void Discount()
        {
            DiscountService.ShowDialog(
            dialogButtons: MessageButton.OKCancel,
            title: "Discount",
            viewModel: this); RaisePropertyChanged();
        }
        [Command]
        public void ChangePrice()
        {
              DialogService.ShowDialog(
              dialogButtons: MessageButton.OKCancel,
              title: "Change Price",
              viewModel: this
          );
            RaisePropertyChanged();
        }
        [Command]
        public void ChangeQty()
        {
            DialogService1.ShowDialog(
             dialogButtons: MessageButton.OKCancel,
             title: "Change quantity",
             viewModel: this);
            RaisePropertyChanged();
        }
            [Command]
            public void CellValueChangedCommand()
            {
                RaisePropertyChanged();
            }
            [Command]
            public void ChangeCustomer()
            {
                        MessageResult result = CustomerService.ShowDialog(
                         dialogButtons: MessageButton.OKCancel,
                         title: "Change Customer",
                         viewModel: this
                     );
                if (result == MessageResult.OK)
                {
               
                }
             }

        [Command]
        public void VoidInvoice()
        {
            
        }
        [Command]
        public async void SaveInvoice()
        {
            
            unitOfWork.SalesRepository.SaveSale(new Order
            {
                OrderId= OrderId,
                CustomerId = CustomerId,
                OrderNo= OrderNo,
                SalesOutletId = unitOfWork.SalesOutletRepository.Get().FirstOrDefault().SalesOutletId,
                StaffId = unitOfWork.StaffRepo.Get().FirstOrDefault().StaffId,
                VAT =TotalVAT,
                Discount =TotalDiscount,
                OrderAmount = SubTotal,
                TotalAmount= Total
            },ItemCollection.ToList());
        }
    }
}
