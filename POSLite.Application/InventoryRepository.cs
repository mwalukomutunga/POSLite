using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using POSLite.Domain;
using POSLite.Persistance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSLite.App
{
    public interface IInventoryRepository : IRepository<ItemInventory>
    {
        Task AdjustInventory(InventoryAdjustment inventory);
        List<InventoryAdjustment> FetchInvAdjustment();
        float GetItemsInStock(object id);
         Task ChangeQty(Item item, float Qty, SalesOutlet salesOutlet, string reference);
    }
    public  class InventoryRepository :  Repository<ItemInventory>, IInventoryRepository
    {
        public InventoryRepository(AppDataContext _context) : base(_context)
        { }

        public async Task AdjustInventory(InventoryAdjustment inventory)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())

            {
                await context.AddAsync(inventory);
                await context.SaveChangesAsync();

                await context.ItemInventories.AddAsync(new ItemInventory { ItemId= inventory.ItemId, Quantity = inventory.AdjQty,QuantityInStock= GetItemsInStock(inventory.ItemId) + inventory.AdjQty, Reference="Inventory Adjustment",CostCenterId=inventory.CostCenterId,OtherDetails= "Inventory Adjustment" });
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }

        public float GetItemsInStock(object Id)
        {
            var items =  context.ItemInventories.Where(x => x.ItemId == (Guid)Id);
            if (items.Count()==0) return 0;
            DateTime Latest = items.Max(x => x.InventoryDate);
            return  items.FirstOrDefault(x=>x.InventoryDate== Latest).QuantityInStock; 
        }

        List<InventoryAdjustment> IInventoryRepository.FetchInvAdjustment()
        {
            return context.InventoryAdjustment.ToList();
        }
        public override ValueTask<EntityEntry<ItemInventory>> Insert(ItemInventory entity)
        {
            entity.QuantityInStock = GetItemsInStock(entity.ItemId) + entity.Quantity;
            return base.Insert(entity);
        }

        public async Task ChangeQty(Item item, float Qty,SalesOutlet salesOutlet,string reference)
        {
            await context.ItemInventories.AddAsync(new ItemInventory { ItemId = item.ItemId, Quantity = Qty, QuantityInStock = GetItemsInStock(item.ItemId) + Qty, Reference = reference, CostCenterId = salesOutlet.SalesOutletId, OtherDetails = reference });
            await context.SaveChangesAsync();
        }
    }
}
