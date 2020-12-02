using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using POSLite.App;
using POSLite.Domain;
using POSLite.Persistance;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace POSLite.Client.ViewModels
{
   public class CompanyViewModel : BaseViewModel
    {
        public CompanyViewModel()
        {
           
            SelectedObject = new Company() { ID = new Guid() };
            GridCollection = new ObservableCollection<object>(unitOfWork.CompanyRepository.Get());
        }

        Company _SelectedObject;
        public Company SelectedObject
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
            Company item = unitOfWork.CompanyRepository.GetByID(SelectedObject.ID).Result;
            return IsNewRecord = item == null;
        }

        [Command]
        public async Task Save()
        {
            try
            {
                await unitOfWork.CompanyRepository.Insert(SelectedObject);
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
                SelectedObject = new Company() { };
            }

        }


        [Command]
        public async Task Delete()
        {
            try
            {
                await unitOfWork.CompanyRepository.DeleteAsync(SelectedObject.ID);
                await unitOfWork.SaveAsync();
                GridCollection.Remove(SelectedObject);
                MessageBoxService.Show("Company record Deleted");
            }
            catch (Exception ex)
            {

                MessageBoxService.Show(ex.InnerException.Message);
            }
            finally
            {
                SelectedObject = new Company() { };
            }

        }

        [Command]
        public void New()
        {
            SelectedObject = new Company() { ID = new Guid() };
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
                unitOfWork.CompanyRepository.Update(SelectedObject);
                await unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                SelectedObject = new Company() { };
                GridCollection = new ObservableCollection<object>(unitOfWork.CompanyRepository.Get());
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

