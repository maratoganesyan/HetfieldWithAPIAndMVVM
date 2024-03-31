using APIForHetfield;
using Hetfield.Models;
using Hetfield.Tools.DbUtils;
using Hetfield.Tools.MVVMTools;
using Hetfield.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Hetfield.View.AddAndChangeViews;
using System.Reflection;

namespace Hetfield.ViewModel
{
    class EmployeePageClientsVM : EmployeeViewPagesVM<User>
    {

        public EmployeePageClientsVM()
        {
            
        }


        protected override async void Refresh()
        {
            try
            {
                ApiClient apiClient = new ApiClient();
                var values = await apiClient.GetAllEntityData<User>();
                TableValue = new ObservableCollection<User>(values.Where(u => u.IdRole == AllRoles.Client));
            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    new CustomMessageBoxView("Ошибка запроса на сервер").ShowDialog();
                });
            }
            
        }

        protected override async void SearchData(string searchText)
        {
            try
            {
                ApiClient apiClient = new ApiClient();
                var valuesFromDb = await apiClient.GetSearchedEntityDataAsync<User>(searchText);
                var clients = valuesFromDb.ToList().Where(u => u.IdRole == 1);
                TableValue = new ObservableCollection<User>(clients);
            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    new CustomMessageBoxView("Ошибка запроса на сервер").ShowDialog();
                });
            }
        }

        protected override void OpenAddDialog(object parameter)
        {
            ContentDialogService.userControlInDialog = new UserAddAndChange(true);
            ContentDialogService service = new ContentDialogService();
            service.OpenDialog();
        }

        protected override void OpenChangeDialog(object parameter)
        {
            if (parameter is object[] parameters && parameters.Length == 2)
            {
                if (parameters[1] is Type userControlType && typeof(UserControl).IsAssignableFrom(userControlType) && parameters[0] is Button button)
                {
                    ConstructorInfo constructor = userControlType.GetConstructor(new Type[] { typeof(User) });
                    if (constructor != null)
                    {
                        User value = (User)button.DataContext;
                        ContentDialogService.userControlInDialog = new UserAddAndChange(value);
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
    }
}
