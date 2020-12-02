using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using Autofac;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Microsoft.Win32.SafeHandles;
using POSLite.App;
using POSLite.Client.Classes;
using POSLite.Persistance;

namespace POSLite.Client.ViewModels
{
  public abstract class BaseViewModel: ViewModelBase, IDisposable
    {
        public IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(); } }
        public IDispatcherService DispatcherService { get { return this.GetService<IDispatcherService>(); } }
        public IUnitOfWork unitOfWork = DependencyContainer.Container.Resolve<IUnitOfWork>(); //new UnitOfWork(DependencyContainer.Container.Resolve<AppDataContext>());
        public bool IsNewRecord { get; set; }
        private ObservableCollection<object> _Collection;

        public ObservableCollection<object> GridCollection
        {
            get { return _Collection; }
            set { SetValue(ref _Collection, value); }
        }
        // Flag: Has Dispose already been called?
        internal  bool disposed = false;
        // Instantiate a SafeHandle instance.
        internal SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected abstract void Dispose(bool disposing);

       
    }
   
}
