using POSLite.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace POSLite.Client.ViewModels
{
    public class PaymentsViewModel : BaseViewModel
    {
        public PaymentsViewModel()
        {
            PaymentMethods = new ObservableCollection<PaymentMethod>(unitOfWork.PaymentMethods.Get());
        }

        public ObservableCollection<PaymentMethod> PaymentMethods { get; set; }
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
}
