using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using POSLite.App;
using POSLite.Domain;
using POSLite.Persistance;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace POSLite.Client.ViewModels
{
    public class SalesOutletViewModel : BaseViewModel
    {
        public SalesOutletViewModel()
        {
            CompanyData = new ObservableCollection<Company>(unitOfWork.CompanyRepository.Get());
            SelectedObject = new SalesOutlet() { SalesOutletId = new Guid(), CompanyId= CompanyData.FirstOrDefault().ID };
            GridCollection = new ObservableCollection<object>(unitOfWork.SalesOutletRepository.Get());
        }
        public ObservableCollection<Company> CompanyData { get; set; }
        SalesOutlet _SelectedObject;
        public SalesOutlet SelectedObject
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
            SalesOutlet item = unitOfWork.SalesOutletRepository.GetByID(SelectedObject.SalesOutletId).Result;
            return IsNewRecord = item == null;
        }

        [Command]
        public async Task Save()
        {
            try
            {
                await unitOfWork.SalesOutletRepository.Insert(SelectedObject);
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
                SelectedObject = new SalesOutlet() { };
            }

        }


        [Command]
        public async Task Delete()
        {
            try
            {
                await unitOfWork.SalesOutletRepository.DeleteAsync(SelectedObject.SalesOutletId);
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
                SelectedObject = new SalesOutlet() { };
            }

        }

        [Command]
        public void New()
        {
            SelectedObject = new SalesOutlet() { SalesOutletId = new Guid() };
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
                unitOfWork.SalesOutletRepository.Update(SelectedObject);
                await unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                SelectedObject = new SalesOutlet() { };
                GridCollection = new ObservableCollection<object>(unitOfWork.SalesOutletRepository.Get());
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


    }
}
