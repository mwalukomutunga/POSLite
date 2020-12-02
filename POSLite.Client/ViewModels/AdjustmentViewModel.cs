using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using POSLite.App;
using POSLite.Domain;
using POSLite.Persistance;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSLite.Client.ViewModels
{
   public class AdjustmentViewModel: BaseViewModel
    {
        public AdjustmentViewModel()
        {
            try
            {

                ItemsData = new ObservableCollection<Item>(unitOfWork.ItemsRepository.Get());
                SalesOutletData = new ObservableCollection<SalesOutlet>(unitOfWork.SalesOutletRepository.Get());


                SelectedObject = new InventoryAdjustment() { ID = new Guid(), ItemId=ItemsData.FirstOrDefault().ItemId,CostCenterId= SalesOutletData.FirstOrDefault().SalesOutletId };
                GridCollection = new ObservableCollection<object>(unitOfWork.InventoryRepository.FetchInvAdjustment());
            }
            catch (Exception)
            {

              
            }
        }

        public ObservableCollection<Item> ItemsData { get; set; }
        public ObservableCollection<SalesOutlet> SalesOutletData { get; set; }

        InventoryAdjustment _SelectedObject;
        public InventoryAdjustment SelectedObject
        {
            get { return _SelectedObject; }
            set
            {
                SetValue(ref _SelectedObject, value);
            }
        }

        //public bool CanSave()
        //{
        //    if (SelectedObject == null)
        //    {
        //        return IsNewRecord = false;
        //    }
        //    return IsNewRecord = unitOfWork.InventoryRepository.IsNewItem(SelectedObject.ID).Result;
        //}

        [Command]
        public async Task Save()
        {
            try
            {
                await unitOfWork.InventoryRepository.AdjustInventory(SelectedObject);
                await unitOfWork.SaveAsync();
                GridCollection.Insert(0, SelectedObject);
                MessageBoxService.Show("Inventory updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBoxService.Show(ex.InnerException.Message);
            }
            finally
            {
                SelectedObject = new InventoryAdjustment() { ID = new Guid(), ItemId = ItemsData.FirstOrDefault().ItemId, CostCenterId = SalesOutletData.FirstOrDefault().SalesOutletId };
            }

        }


  

        [Command]
        public void New()
        {
            SelectedObject = new InventoryAdjustment() { ID = new Guid(), ItemId = ItemsData.FirstOrDefault().ItemId, CostCenterId = SalesOutletData.FirstOrDefault().SalesOutletId };
        }
        public bool CanUpdate()
        {
            return !IsNewRecord;
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

