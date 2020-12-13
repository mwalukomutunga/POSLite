using POSLite.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace POSLite.Client.ViewModels
{
    public class PaymentsViewModel : BaseViewModel
    {
        private Invoice _invoice;

        public Invoice Invoice
        {
            get { return _invoice; }
            set { SetValue(ref _invoice, value); }
        }

        public PaymentsViewModel()
        {
            PaymentMethods = new ObservableCollection<PayMethods>(LoadPayTypes(unitOfWork.PaymentMethods.Get()));           
        }
        public IEnumerable<PayMethods> LoadPayTypes(IEnumerable<PaymentMethod> paymentMethods)
        {
            foreach (var item in paymentMethods)
            {
                yield return new PayMethods
                {
                    PaymetId = item.ID,
                     Name = item.PaymentMethodName,
                     Icon= item.Icon,
                };
            }
        }
        public ObservableCollection<PayMethods> PaymentMethods { get; set; }
        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //unitOfWork.Dispose();
            }

            disposed = true;
        }
    }

    public class PayMethods : INotifyPropertyChanged
    {
        private float amount;

        public Guid PaymetId { get; set; }
        public string Name { get; set; }
        public float Amount { get => amount; set {
                amount = value;
                NotifyPropertyChanged();
            } }
        public byte[] Icon { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
