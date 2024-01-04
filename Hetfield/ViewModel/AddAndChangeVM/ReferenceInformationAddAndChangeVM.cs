using APIForHetfield;
using Hetfield.Models;
using Hetfield.Tools;
using Hetfield.Tools.MVVMTools;
using Hetfield.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Hetfield.ViewModel
{
    internal class ReferenceInformationAddAndChangeVM<TTable> : ViewModelBase where TTable : DbModelBase, new()
    {
        private bool addMode;

        private TTable _tableValue;

        public TTable TableValue
        {
            get => _tableValue;
            set
            {
                _tableValue = value;
                OnPropertyChanged(nameof(TableValue));
            }
        }

        #region constructors
        public ReferenceInformationAddAndChangeVM(TTable tableValue)
        {
            TableValue = tableValue;
            addMode = false;
        }

        public ReferenceInformationAddAndChangeVM()
        {
            addMode = true;
            TableValue = new();
        }
        #endregion



        public RelayCommand SaveChangesCommand => new RelayCommand(obj => new Thread(Save).Start());

        public RelayCommand CloseCommand => new RelayCommand(obj => CloseDialog()); 
        protected async void Save()
        {
            if (addMode)
                Add();
            else
                Change();
        }

        private async void Add()
        {
            ApiClient apiClient = new ApiClient();
            if(await TableValue.Validate(addMode))
            {
                bool IsDataAdded;
                try
                {
                    IsDataAdded = await apiClient.AddEntityDataAsync<TTable>(_tableValue);
                }
                catch(Exception ex)
                {
                    IsDataAdded = false;
                }
                if(IsDataAdded)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        new CustomMessageBoxView("Данные успешно сохранены").ShowDialog();
                        CloseDialog();
                    });
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        new CustomMessageBoxView("Проблемы с откликом сервера. Повторите попытку позже").ShowDialog();
                        CloseDialog();
                    });
                }

            }
        }

        private async void Change()
        {
            ApiClient apiClient = new ApiClient();
            if (await TableValue.Validate(addMode))
            {
                bool IsDataChanged;
                try
                {
                    IsDataChanged = await apiClient.ChangeEntityDataAsync<TTable>(_tableValue);
                }
                catch(Exception ex)
                {
                    IsDataChanged = false;
                }
                if (IsDataChanged)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        new CustomMessageBoxView("Данные успешно изменены").ShowDialog();
                        CloseDialog();
                    });
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        new CustomMessageBoxView("Проблемы с откликом сервера. Повторите попытку позже").ShowDialog();
                        CloseDialog();
                    });
                }
            }
        }

        private void CloseDialog()
        {
            ContentDialogService service = new ContentDialogService();
            service.CloseDialog();
        }

        

    }
}
