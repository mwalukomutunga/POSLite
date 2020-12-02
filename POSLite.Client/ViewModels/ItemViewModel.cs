using Autofac;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using POSLite.App;
using POSLite.Client.Classes;
using POSLite.Domain;
using POSLite.Persistance;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace POSLite.Client.ViewModels
{
    public class ItemViewModel : BaseViewModel
    {
        protected IDialogService DialogService { get { return this.GetService<IDialogService>(); } }
        public ItemViewModel()
        {
          
            Categories = new ObservableCollection<ItemCategory>(unitOfWork.CategoryRepo.Get());
            Brands = new ObservableCollection<Brand>(unitOfWork.BrandRepo.Get());
            UOM = new ObservableCollection<UnitOfMeasurement>(unitOfWork.UOMRepository.Get());

            SelectedObject = new Item() { ItemId = new Guid(), UOMCodeId = UOM.FirstOrDefault().ID, BrandId = Brands.FirstOrDefault().BrandId, CategoryId= Categories.FirstOrDefault().CategoryId};
            GridCollection = new ObservableCollection<object>(unitOfWork.ItemsRepository.Get());
           
        }

        public ObservableCollection<ItemCategory> Categories { get; set; }
        public ObservableCollection<Brand> Brands { get; set; }
        public ObservableCollection<UnitOfMeasurement> UOM { get; set; }

        Item _SelectedObject;

        public Item SelectedObject
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
            Item item = unitOfWork.ItemsRepository.GetByID(SelectedObject.ItemId).Result;
            return IsNewRecord = item == null;
        }

        [Command]
        public async Task Save()
        {
            try
            {
                await unitOfWork.ItemsRepository.SaveItems(SelectedObject);
                GridCollection.Insert(0, SelectedObject);
                MessageBoxService.Show("Category record saved");
            }
            catch (Exception ex)
            {
                MessageBoxService.Show(ex.InnerException.Message);
            }
            finally
            {
                SelectedObject = new Item() { };
            }
        }
        public bool CanChangePrice()
        {
            return !IsNewRecord;
        }
        [Command]
       public async Task ChangePrice()
        {
            MessageResult result = DialogService.ShowDialog(
              dialogButtons: MessageButton.OKCancel,
              title: "Change item details",
              viewModel: this
          );
            if (result== MessageResult.OK)
            {
                var retail = SelectedObject.RetailPrice;
                await unitOfWork.ItemsRepository.ChangePriceAsync(SelectedObject);
                GridCollection = new ObservableCollection<object>(unitOfWork.ItemsRepository.Get());
                MessageBoxService.ShowMessage("You have changed the price successfully.");
            }
           
        }
        public bool CanDelete()
        {
            return !IsNewRecord;
        }
        [Command]
        public async Task Delete()
        {
            try
            {
                await unitOfWork.ItemsRepository.SoftDeleteAsync(SelectedObject.ItemId);
                GridCollection.Remove(SelectedObject);
                MessageBoxService.Show("Category record Deleted");
            }
            catch (Exception ex)
            {

                MessageBoxService.Show(ex.Message);
            }
            finally
            {
                SelectedObject = new Item() { };
            }

        }

        [Command]
        public void New()
        {
            SelectedObject = new Item() { ItemId = new Guid() };
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
                unitOfWork.ItemsRepository.Update(SelectedObject);
                await unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                SelectedObject = new Item() { };
                GridCollection = new ObservableCollection<object>(unitOfWork.ItemsRepository.Get());
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
