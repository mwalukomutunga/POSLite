using POSLite.Domain;
using POSLite.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSLite.App
{
    public interface ISaleRepository
    {
        string GenerateOrderNo();
        Task<Invoice> SaveSale(Order order, List<OrderItem> items);
    }
    public class SaleRepository : ISaleRepository
    {
        private AppDataContext context;

        public SaleRepository(AppDataContext _context)
        { context = _context; }

        public string GenerateOrderNo()
        {
            string result="";
            IQueryable<Order> query = context.Set<Order>();
            try
            {
                var lastRecord = query.Where(x => x.CreatedAt == query.Max(x => x.CreatedAt)).FirstOrDefault();
                if(lastRecord !=null)
                    result =$"OR{ (Convert.ToInt32(lastRecord.OrderNo[3..]) +1).ToString().PadLeft(4, '0')}";
            }
            catch (Exception)
            {

                result = "OR0001";
            }

            return result;// (lastRecord != null) ? $"INV{str+1}" : "INV001";
        }
        private string GenerateInvoiceNo()
        {
            string result = "";
            IQueryable<Invoice> query = context.Set<Invoice>();
            try
            {
                var lastRecord = query.Where(x => x.CreatedAt == query.Max(x => x.CreatedAt)).FirstOrDefault();
                if (lastRecord != null)
                    result = $"INV{ (Convert.ToInt32(lastRecord.InvoiceNo[3..]) + 1).ToString().PadLeft(4, '0')}";
            }
            catch (Exception)
            {

                result = "INV0001";
            }

            return result;// (lastRecord != null) ? $"INV{str+1}" : "INV001";
        }
        async System.Threading.Tasks.Task<Invoice> ISaleRepository.SaveSale(Order order, List<OrderItem> items)
        {
            Invoice invoice;
            try
            {
                 invoice = new Invoice
                {
                    InvoiceId = Guid.NewGuid(),
                    InvoiceNo = GenerateInvoiceNo(),
                    CustomerId = order.CustomerId,
                    SalesOutletId = order.SalesOutletId,
                    StaffId = order.StaffId,
                    VAT = order.VAT,
                    Discount = order.Discount,
                    InvoiceAmount = order.OrderAmount,
                    TotalAmount = order.TotalAmount,

                };
                
                using (var transaction = await context.Database.BeginTransactionAsync())

                {
                    await context.AddAsync(order);
                    await context.SaveChangesAsync();

                    await context.AddRangeAsync(items);
                    await context.SaveChangesAsync();

                    await context.AddAsync(invoice);
                    await context.SaveChangesAsync();

                    await context.AddRangeAsync(LoadItems(invoice, items).ToList());
                    await context.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
            }
            catch (Exception ex)
            {
                
                return null;
            }
            return invoice;
        }
        private IEnumerable<InvoiceLineItem> LoadItems(Invoice invoice, List<OrderItem> items)
        {
            foreach (var item in items)
            {
                yield return new InvoiceLineItem
                {
                    InvoiceId = invoice.InvoiceId,
                    ItemId = item.ItemId,
                    Name=item.Description,
                    Qty = item.Qty,
                    Price = item.Price,
                    VAT = item.VAT,
                    Discount = item.Discount,
                    Total = item.Total
                };
            }
        }
       
    }
}
