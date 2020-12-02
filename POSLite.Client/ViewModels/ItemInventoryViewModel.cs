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
    public class ItemInventoryViewModel : BaseViewModel
    {
        public ItemInventoryViewModel()
        {
            try
            {
              
                ItemsData = new ObservableCollection<Item>(unitOfWork.ItemsRepository.Get());
                SalesOutletData = new ObservableCollection<SalesOutlet>(unitOfWork.SalesOutletRepository.Get());

                SelectedObject = new ItemInventory() { InventoryId = new Guid(), ItemId = ItemsData.FirstOrDefault().ItemId, CostCenterId = SalesOutletData.FirstOrDefault().SalesOutletId,Reference="Opening stock",OtherDetails="Remarks" };
                GridCollection = new ObservableCollection<object>(unitOfWork.InventoryRepository.Get(x=>x.Reference== "Opening stock"));
            }
            catch (Exception)
            {

               
            }
        }
        public ObservableCollection<Item> ItemsData { get; set; }
        public ObservableCollection<SalesOutlet> SalesOutletData { get; set; }
        ItemInventory _SelectedObject;
        public ItemInventory SelectedObject
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
            ItemInventory item = unitOfWork.InventoryRepository.GetByID(SelectedObject.InventoryId).Result;
            return IsNewRecord = item == null;
        }

        [Command]
        public async Task Save()
        {
            try
            {
                await unitOfWork.InventoryRepository.Insert(SelectedObject);
                await unitOfWork.SaveAsync();
                GridCollection.Insert(0, SelectedObject);
                MessageBoxService.Show("Category record saved");
            }
            catch (Exception ex)
            {
                MessageBoxService.Show(ex.Message);
            }
            finally
            {
                SelectedObject = new ItemInventory() { InventoryId = new Guid(), ItemId = ItemsData.FirstOrDefault().ItemId, CostCenterId = SalesOutletData.FirstOrDefault().SalesOutletId, Reference = "Opening stock", OtherDetails = "Remarks" };
            }

        }


        [Command]
        public async Task Delete()
        {
            try
            {
                await unitOfWork.InventoryRepository.DeleteAsync(SelectedObject.InventoryId);
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
                SelectedObject = new ItemInventory() { InventoryId = new Guid(), ItemId = ItemsData.FirstOrDefault().ItemId, CostCenterId = SalesOutletData.FirstOrDefault().SalesOutletId, Reference = "Opening stock", OtherDetails = "Remarks" };
            }

        }

        [Command]
        public void New()
        {
            SelectedObject = new ItemInventory() { InventoryId = new Guid(), ItemId = ItemsData.FirstOrDefault().ItemId, CostCenterId = SalesOutletData.FirstOrDefault().SalesOutletId, Reference = "Opening stock", OtherDetails = "Remarks" };
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
                unitOfWork.InventoryRepository.Update(SelectedObject);
                await unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                SelectedObject = new ItemInventory() { };
                GridCollection = new ObservableCollection<object>(unitOfWork.InventoryRepository.Get(x => x.Reference == "Opening stock"));
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
