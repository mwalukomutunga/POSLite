﻿using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using POSLite.App;
using POSLite.Domain;
using POSLite.Persistance;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace POSLite.Client.ViewModels
{
    public class UnitOfMeasurementViewModel : BaseViewModel
    {
        public UnitOfMeasurementViewModel()
        {
            SelectedObject = new UnitOfMeasurement() { ID = new Guid() };
            GridCollection = new ObservableCollection<object>(unitOfWork.UOMRepository.Get());
        }

        UnitOfMeasurement _SelectedObject;
        public UnitOfMeasurement SelectedObject
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
            UnitOfMeasurement item = unitOfWork.UOMRepository.GetByID(SelectedObject.ID).Result;
            return IsNewRecord = item == null;
        }

        [Command]
        public async Task Save()
        {
            try
            {
                await unitOfWork.UOMRepository.Insert(SelectedObject);
                await unitOfWork.SaveAsync();
                GridCollection.Insert(0, SelectedObject);
                MessageBoxService.Show("Category record saved");
            }
            catch (Exception ex)
            {
                MessageBoxService.Show(ex.InnerException.Message);
            }
            finally
            {
                SelectedObject = new UnitOfMeasurement() { };
            }

        }


        [Command]
        public async Task Delete()
        {
            try
            {
                await unitOfWork.UOMRepository.DeleteAsync(SelectedObject.ID);
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
                SelectedObject = new UnitOfMeasurement() { };
            }

        }

        [Command]
        public void New()
        {
            SelectedObject = new UnitOfMeasurement() { ID = new Guid() };
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
                unitOfWork.UOMRepository.Update(SelectedObject);
                await unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                SelectedObject = new UnitOfMeasurement() { };
                GridCollection = new ObservableCollection<object>(unitOfWork.UOMRepository.Get());
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
                //unitOfWork.Dispose();
            }

            disposed = true;
        }

    }

    }
