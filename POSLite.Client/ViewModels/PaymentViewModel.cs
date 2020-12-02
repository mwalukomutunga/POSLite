using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using POSLite.App;
using POSLite.Domain;
using POSLite.Persistance;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace POSLite.Client.ViewModels
{
    public class PaymentViewModel : BaseViewModel
    {
        public PaymentViewModel()
        {
            
            SelectedObject = new Payment() { PaymentId = new Guid() };
            GridCollection = new ObservableCollection<object>(unitOfWork.PaymentRepository.Get());
        }

        Payment _SelectedObject;
        public Payment SelectedObject
        {
            get { return _SelectedObject; }
            set
            {
                SetValue(ref _SelectedObject, value);
            }
        }

        public bool CanSave()
        {
            if (SelectedObject == null)
            {
                return IsNewRecord = false;
            }
            Payment item = unitOfWork.PaymentRepository .GetByID(SelectedObject.PaymentId).Result;
            return IsNewRecord = item == null;
        }

        [Command]
        public async Task Save()
        {
            try
            {
                await unitOfWork.PaymentRepository .Insert(SelectedObject);
                await unitOfWork.SaveAsync();
                GridCollection.Insert(0, SelectedObject);
                MessageBoxService.Show("Category record saved");
            }
            catch (Exception ex)
            {
                MessageBoxService.Show(ex.InnerException.Message);
            }
            finally
            {
                SelectedObject = new Payment() { };
            }

        }


        [Command]
        public async Task Delete()
        {
            try
            {
                await unitOfWork.PaymentRepository .DeleteAsync(SelectedObject.PaymentId);
                await unitOfWork.SaveAsync();
                GridCollection.Remove(SelectedObject);
                MessageBoxService.Show("Category record Deleted");
            }
            catch (Exception ex)
            {

                MessageBoxService.Show(ex.InnerException.Message);
            }
            finally
            {
                SelectedObject = new Payment() { };
            }

        }

        [Command]
        public void New()
        {
            SelectedObject = new Payment() { PaymentId = new Guid() };
        }
        public bool CanUpdate()
        {
            return !IsNewRecord;
        }
        [Command]
        public async Task Update()
        {
            try
            {
                unitOfWork.PaymentRepository .Update(SelectedObject);
                await unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                SelectedObject = new Payment() { };
                GridCollection = new ObservableCollection<object>(unitOfWork.PaymentRepository .Get());
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
              // unitOfWork.Dispose();
            }

            disposed = true;
        }

    }
}
