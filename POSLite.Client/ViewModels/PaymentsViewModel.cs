using Autofac;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using POSLite.Client.Classes;
using POSLite.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace POSLite.Client.ViewModels
{
    public class PaymentsViewModel : BaseViewModel
    {
        private Invoice _invoice;
        private string error;
        public string Error { get => error; set { SetValue(ref error, value); } }
        public DelegateCommand<Window> SaveCommand { get; private set; }
        public Invoice Invoice
        {
            get { return _invoice; }
            set { SetValue(ref _invoice, value); }
        }

        public PaymentsViewModel()
        {
            SaveCommand = new DelegateCommand<Window>(SavePayment);
            PaymentMethods = new ObservableCollection<PayMethods>(LoadPayTypes(unitOfWork.PaymentMethods.Get()));
            Error = "";
        }

        private async void SavePayment(Window obj)
        {
            try
            {
                foreach (var item in PaymentMethods.Where(x => x.Amount > 0))
                {
                    await unitOfWork.PaymentRepository.Insert(new Payment
                    {
                        PaymentId = Guid.NewGuid(),
                        CustomerId = Invoice.CustomerId,
                        InvoiceId = Invoice.InvoiceId,
                        PaymentMethodId = item.PaymetId,
                        Status = Domain.Enums.PaymentStatus.Completed,
                        PaymentAmount = item.Amount
                    });
                    await unitOfWork.SaveAsync();
                    obj.Close();
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
        }

        public IEnumerable<PayMethods> LoadPayTypes(IEnumerable<PaymentMethod> paymentMethods)
        {
            foreach (var item in paymentMethods)
            {
                yield return new PayMethods
                {
                    PaymetId = item.ID,
                    Name = item.PaymentMethodName,
                    Icon = item.Icon,
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
