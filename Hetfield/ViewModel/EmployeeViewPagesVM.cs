using APIForHetfield;
using Hetfield.Models;
using Hetfield.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Hetfield.Tools.MVVMTools;
using Hetfield.View.AddAndChangeViews;
using Hetfield.View.Pages;
using System.Windows.Controls;
using System.Reflection;
using Hetfield.View;

namespace Hetfield.ViewModel
{
    class EmployeeViewPagesVM<TTable> : ViewModelBase where TTable : DbModelBase 
    {
        #region fields
        private ObservableCollection<TTable> _tableValue;

        private string _searchingText;

        #endregion

        #region props
        public ObservableCollection<TTable> TableValue
        {
            get => _tableValue;
            set
            {
                _tableValue = value;
                OnPropertyChanged(nameof(TableValue));
            }
        }

        public string SearchingText
        {
            get => _searchingText;
            set
            {
                _searchingText = value;
                OnPropertyChanged(nameof(SearchingText));
            }
        }

        #endregion

        public EmployeeViewPagesVM()
        {
            Refresh();
        }


        #region Commands
        public RelayCommand SearchDataCommand => new RelayCommand(obj => new Thread(() => SearchData(SearchingText)).Start());

        public RelayCommand OpenAddDialogCommand => new RelayCommand(obj => OpenAddDialog(obj));

        public RelayCommand OpenChangeDialogCommand => new RelayCommand(obj => OpenChangeDialog(obj));

        public RelayCommand RefreshCommand => new RelayCommand(obj => Refresh());

        #endregion

        #region Methods
        protected virtual async void Refresh()
        {
            try
            {
                ApiClient apiClient = new ApiClient();
                var values = await apiClient.GetAllEntityData<TTable>();
                TableValue = new ObservableCollection<TTable>(values);
            }
            catch(Exception ex)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    new CustomMessageBoxView("Ошибка запроса на сервер").ShowDialog();
                });
            }
            
        }

        protected virtual async void SearchData(string searchText)
        {
            try
            {
                ApiClient apiClient = new ApiClient();
                var data = await apiClient.GetSearchedEntityDataAsync<TTable>(searchText);
                TableValue = new ObservableCollection<TTable>(data.ToList());
            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    new CustomMessageBoxView("Ошибка запроса на сервер").ShowDialog();
                });
            }
        }

        protected virtual void OpenAddDialog(object parameter)
        {
            if (parameter is Type userControlType && typeof(UserControl).IsAssignableFrom(userControlType))
            {
                try
                {
                    ContentDialogService.userControlInDialog = (UserControl)Activator.CreateInstance(userControlType);
                    ContentDialogService service = new ContentDialogService();
                    service.OpenDialog();
                }
                catch (Exception ex)
                {
                    new CustomMessageBoxView("Ошибка при открытии страницы").ShowDialog();
                }
            }
            else
            {

            }
        }

        protected virtual void OpenChangeDialog(object parameter)
        {
            if(parameter is object[] parameters && parameters.Length == 2)
            {
                if (parameters[1] is Type userControlType && typeof(UserControl).IsAssignableFrom(userControlType) && parameters[0] is Button button)
                {
                    ConstructorInfo constructor = userControlType.GetConstructor(new Type[] { typeof(TTable) });
                    if (constructor != null)
                    {
                        TTable value = (TTable)button.DataContext;    
                        ContentDialogService.userControlInDialog = (UserControl)constructor.Invoke(new object[] { value });
                        ContentDialogService service = new ContentDialogService();
                        service.OpenDialog();
                    }
                    else
                    {
                        new CustomMessageBoxView("Ошибка при открытии страницы").ShowDialog();
                    }
                }
            }

        }

        #endregion

    }
}
