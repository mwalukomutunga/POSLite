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
    public class BrandViewModel : BaseViewModel
    {
        public BrandViewModel()
        {
           
            SelectedObject = new Brand() { BrandId = new Guid() };
            GridCollection = new ObservableCollection<object>(unitOfWork.BrandRepo.Get());
        }

        Brand _SelectedObject;
        public Brand SelectedObject
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
            Brand item = unitOfWork.BrandRepo.GetByID(SelectedObject.BrandId).Result;
            return IsNewRecord = item == null;
        }

        [Command]
        public async Task Save()
        {
            try
            {
                await unitOfWork.BrandRepo.Insert(SelectedObject);
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
                SelectedObject = new Brand() { };
            }

        }


        [Command]
        public async Task Delete()
        {
            try
            {
                await unitOfWork.BrandRepo.DeleteAsync(SelectedObject.BrandId);
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
                SelectedObject = new Brand() { };
            }

        }

        [Command]
        public void New()
        {
            SelectedObject = new Brand() { BrandId = new Guid() };
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
                unitOfWork.BrandRepo.Update(SelectedObject);
                await unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                SelectedObject = new Brand() { };
                GridCollection = new ObservableCollection<object>(unitOfWork.BrandRepo.Get());
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
