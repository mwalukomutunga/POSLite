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
    public class CustomerViewModel:BaseViewModel
    {
        public CustomerViewModel()
        {
          
            SelectedObject = new Customer() { CustomerId = new Guid() };
            GridCollection = new ObservableCollection<object>(unitOfWork.CustomerRepository.Get());
        }

        Customer _SelectedObject;
        public Customer SelectedObject
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
            Customer item = unitOfWork.CustomerRepository.GetByID(SelectedObject.CustomerId).Result;
            return IsNewRecord = item == null;
        }

        [Command]
        public async Task Save()
        {
            try
            {
                await unitOfWork.CustomerRepository.Insert(SelectedObject);
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
                SelectedObject = new Customer() { };
            }

        }


        [Command]
        public async Task Delete()
        {
            try
            {
                await unitOfWork.CustomerRepository.DeleteAsync(SelectedObject.CustomerId);
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
                SelectedObject = new Customer() { };
            }

        }

        [Command]
        public void New()
        {
            SelectedObject = new Customer() { CustomerId = new Guid() };
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
                unitOfWork.CustomerRepository.Update(SelectedObject);
                await unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                SelectedObject = new Customer() { };
                GridCollection = new ObservableCollection<object>(unitOfWork.CustomerRepository.Get());
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
