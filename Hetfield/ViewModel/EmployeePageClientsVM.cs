using APIForHetfield;
using Hetfield.Models;
using Hetfield.Tools.DbUtils;
using Hetfield.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

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
    }
}
