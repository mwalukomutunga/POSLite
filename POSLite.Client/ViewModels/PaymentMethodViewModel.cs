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
    public class PaymentMethodViewModel : BaseViewModel
    {
        public PaymentMethodViewModel()
        {
           
            SelectedObject = new PaymentMethod() { ID = new Guid() };
            GridCollection = new ObservableCollection<object>(unitOfWork.PaymentMethods.Get());
        }

        PaymentMethod _SelectedObject;
        public PaymentMethod SelectedObject
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
            PaymentMethod item = unitOfWork.PaymentMethods.GetByID(SelectedObject.ID).Result;
            return IsNewRecord = item == null;
        }

        [Command]
        public async Task Save()
        {
            try
            {
                await unitOfWork.PaymentMethods.Insert(SelectedObject);
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
                SelectedObject = new PaymentMethod() { };
            }

        }


        [Command]
        public async Task Delete()
        {
            try
            {
                await unitOfWork.PaymentMethods.DeleteAsync(SelectedObject.ID);
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
                SelectedObject = new PaymentMethod() { };
            }

        }

        [Command]
        public void New()
        {
            SelectedObject = new PaymentMethod() { ID = new Guid() };
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
                unitOfWork.PaymentMethods.Update(SelectedObject);
                await unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                SelectedObject = new PaymentMethod() { };
                GridCollection = new ObservableCollection<object>(unitOfWork.PaymentMethods.Get());
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
