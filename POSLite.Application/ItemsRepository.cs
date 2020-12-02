using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using POSLite.Domain;
using POSLite.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace POSLite.App
{
    public interface IItemsRepository : IRepository<Item>
    {
        Task<bool> ChangePriceAsync(Item item);
        Task SoftDeleteAsync(object id);
        Task SaveItems(Item entity);
        Item GetItemByBarCode(string barCode);
    }
    public class ItemsRepository : Repository<Item>, IItemsRepository
    {
        public ItemsRepository(AppDataContext _context):base(_context)
        { }
        public async Task<bool> ChangePriceAsync(Item item)
        {
            var product = await context.Items.FindAsync(item.ItemId);
            product.RetailPrice = item.RetailPrice;
            product.Discount = item.Discount;
            product.VAT = item.VAT;
            product.CostPrice = item.CostPrice;
            product.ReorderLevel = item.ReorderLevel;

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    await context.ItemPriceLog.AddAsync(new ItemPriceLog { ItemId = product.ItemId, RetailPrice= product.RetailPrice,Discount= product.Discount,CostPrice= product.CostPrice });
                    await context.SaveChangesAsync();

                    context.Update(product);
                    await context.SaveChangesAsync();

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public Item GetItemByBarCode(string barCode)
        {
            return context.Items.FirstOrDefault(x=>x.BarCode== barCode);
        }
        public async Task SaveItems(Item entity)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())

            {
                await context.AddAsync(entity);
                await context.SaveChangesAsync();
                await context.ItemPriceLog.AddAsync(new ItemPriceLog { Item=null, ItemId= entity.ItemId, RetailPrice = entity.RetailPrice, Discount = entity.Discount, CostPrice = entity.CostPrice });
                await context.SaveChangesAsync();
                await  transaction.CommitAsync();
            }
        }

        public override IEnumerable<Item> Get(Expression<Func<Item, bool>> filter = null, Func<IQueryable<Item>, IOrderedQueryable<Item>> orderBy = null, string includeProperties = "")
        {
            filter = x => x.IsActive;
            return base.Get(filter, orderBy, includeProperties);
        }
        public async Task SoftDeleteAsync(object id)
        {
            Item item = await context.Items.FindAsync(id);
            item.IsActive = false;
            context.Update(item);
            await context.SaveChangesAsync();
           
        }

    }
}
