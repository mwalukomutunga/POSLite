using POSLite.Domain;
using POSLite.Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace POSLite.App
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDataContext context;
        public UnitOfWork(AppDataContext _context)
        {
            context = _context;
        }
        
        #region props
        private  Repository<Customer> _CustomerRepository;
        private  IInventoryRepository inventoryRepository;
        private IItemsRepository itemRepository;
        private ISaleRepository saleRepository;
        private  Repository<Order> orderRepository;
        private  Repository<OrderItem> orderItemsRepository;
        private  Repository<Payment> paymentRepository;
        private  Repository<PaymentMethod> paymentMethods;
        private  Repository<SalesOutlet> salesOutletRepository;
        private Repository<Staff> staffRepo;
        private  Repository<UnitOfMeasurement> uOMRepository;
        private  Repository<Brand> brandRepository;
        private  Repository<ItemCategory> categoryRepository;
        private Repository<Company> _Company;

        public Repository<Customer> CustomerRepository => _CustomerRepository ??= new Repository<Customer>(context);
        public IItemsRepository ItemsRepository => itemRepository ??= new ItemsRepository(context);
        public Repository<Company> CompanyRepository => _Company ??= new Repository<Company>(context);
        public IInventoryRepository InventoryRepository => inventoryRepository ??= new InventoryRepository(context);
        public Repository<Order> OrderRepository => orderRepository ??= new Repository<Order>(context);
        public Repository<OrderItem> OrderItemsRepository => orderItemsRepository ??= new Repository<OrderItem>(context);
        public Repository<Payment> PaymentRepository => paymentRepository ??= new Repository<Payment>(context);
        public Repository<PaymentMethod> PaymentMethods => paymentMethods ??= new Repository<PaymentMethod>(context);
        public Repository<SalesOutlet> SalesOutletRepository => salesOutletRepository ??= new Repository<SalesOutlet>(context);
       // public Repository<SalesTransaction> TransactionsRepository => transactionsRepository ?? new Repository<SalesTransaction>(context);
        public Repository<Staff> StaffRepo => staffRepo ??= new Repository<Staff>(context);
        public Repository<Brand> BrandRepo => brandRepository ??= new Repository<Brand>(context);
        public Repository<ItemCategory> CategoryRepo => categoryRepository ??= new Repository<ItemCategory>(context);
        public Repository<UnitOfMeasurement> UOMRepository => uOMRepository ??= new Repository<UnitOfMeasurement>(context);

        public ISaleRepository SalesRepository =>  saleRepository ??= new SaleRepository(context);
        #endregion props

        public async System.Threading.Tasks.Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;
        

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
