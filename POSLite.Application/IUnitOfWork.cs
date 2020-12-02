using POSLite.Domain;
using POSLite.Persistance;
using System.Threading.Tasks;

namespace POSLite.App
{
    public interface IUnitOfWork
    {
        Repository<Brand> BrandRepo { get; }
        Repository<ItemCategory> CategoryRepo { get; }
        Repository<Customer> CustomerRepository { get; }
        IInventoryRepository InventoryRepository { get; }
        ISaleRepository SalesRepository { get; }
        IItemsRepository ItemsRepository { get; }
        Repository<OrderItem> OrderItemsRepository { get; }
        Repository<Order> OrderRepository { get; }
        Repository<PaymentMethod> PaymentMethods { get; }
        Repository<Payment> PaymentRepository { get; }
        Repository<SalesOutlet> SalesOutletRepository { get; }
        Repository<Staff> StaffRepo { get; }
        Repository<Company> CompanyRepository { get; }
        Repository<UnitOfMeasurement> UOMRepository { get; }

        void Dispose();
        Task SaveAsync();
    }
}