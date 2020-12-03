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
    public class StaffViewModel : BaseViewModel
    {

        public StaffViewModel()
        {
            SalesOutletData = new ObservableCollection<SalesOutlet>(unitOfWork.SalesOutletRepository.Get());
            SelectedObject = new Staff() { StaffId = new Guid() };
            GridCollection = new ObservableCollection<object>(unitOfWork.StaffRepo.Get());
        }
        public ObservableCollection<SalesOutlet> SalesOutletData { get; set; }
        Staff _SelectedObject;
        public Staff SelectedObject
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
            Staff item = unitOfWork.StaffRepo.GetByID(SelectedObject.StaffId).Result;
            return IsNewRecord = item == null;
        }

        [Command]
        public async Task Save()
        {
            try
            {
                await unitOfWork.StaffRepo.Insert(SelectedObject);
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
                SelectedObject = new Staff() { };
            }

        }


        [Command]
        public async Task Delete()
        {
            try
            {
                await unitOfWork.StaffRepo.DeleteAsync(SelectedObject.StaffId);
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
                SelectedObject = new Staff() { };
            }

        }

        [Command]
        public void New()
        {
            SelectedObject = new Staff() { StaffId = new Guid() };
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
                unitOfWork.StaffRepo.Update(SelectedObject);
                await unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                SelectedObject = new Staff() { };
                GridCollection = new ObservableCollection<object>(unitOfWork.StaffRepo.Get());
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
                //unitOfWork.Dispose();
            }

            disposed = true;
        }
    }
}
