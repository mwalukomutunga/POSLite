using Autofac;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.XtraSpreadsheet.Model;
using POSLite.Client.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace POSLite.Client.ViewModels
{
    public enum DocumentType { Items, Customers,Category, Users, Inventory, Orders, Brands, Reports, Sales, Transactions, UOM,InventoryAdjustment, Company, CostCenter,Payments,PayMethods }
    public class MainViewModel : ViewModelBase
    {
        protected IWindowService WindowService { get { return this.GetService<IWindowService>(); } }
        public DelegateCommand<DocumentType> NavCommand { get; private set; }
        public ILifetimeScope Container { get; private set; }
        public object ChildWindowViewModel { get; }

        public MainViewModel()
        {
            NavCommand = new DelegateCommand<DocumentType>(OpenPage);
            Container = DependencyContainer.Container;
        }

        private void OpenPage(DocumentType param)
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                switch (param)
                {
                    case DocumentType.Items:
                        WindowService.Show("Item", scope.Resolve<ItemViewModel>());
                        break;
                    case DocumentType.Customers:
                        WindowService.Show("Customer", scope.Resolve<CustomerViewModel>());
                        break;
                    case DocumentType.Category:
                        WindowService.Show("Category", scope.Resolve<CategoryViewModel>());
                        break;
                    case DocumentType.Users:
                        WindowService.Show("Staff", scope.Resolve <StaffViewModel>());
                        break;
                    case DocumentType.Inventory:
                        WindowService.Show("ItemInventory", scope.Resolve <ItemInventoryViewModel>());
                        break;
                    case DocumentType.Orders:
                        break;
                    case DocumentType.Brands:
                        WindowService.Show("Brand", scope.Resolve <BrandViewModel>());
                        break;
                    case DocumentType.CostCenter:
                        WindowService.Show("SalesOutlet", scope.Resolve <SalesOutletViewModel>());
                        break;
                    case DocumentType.Sales:
                        WindowService.Show("Sales", scope.Resolve<SalesViewModel>());
                        WindowService.Title = "Sales";
                        WindowService.WindowState = DXWindowState.Maximized;
                        break;
                    case DocumentType.Transactions:
                        break;
                    case DocumentType.UOM:
                        WindowService.Show("UnitOfMeasurement", scope.Resolve<UnitOfMeasurementViewModel>());
                        break;
                    case DocumentType.InventoryAdjustment:
                        WindowService.Show("InventoryAdjustment", scope.Resolve<AdjustmentViewModel>());
                        break;
                    case DocumentType.Company:
                        WindowService.Show("Company", scope.Resolve<CompanyViewModel>());
                        break;
                    case DocumentType.Payments:
                        WindowService.Show("Payments", scope.Resolve<PaymentsViewModel>());
                        WindowService.Title = "Payments";
                        break;
                    case DocumentType.PayMethods:
                        WindowService.Show("PaymentMethod", scope.Resolve<PaymentMethodViewModel>());
                        WindowService.Title = "Payment Methods";
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
