using APIForHetfield;
using Hetfield.Models;
using Hetfield.Tools;
using Hetfield.Tools.MVVMTools;
using Hetfield.View;
using Hetfield.View.AddAndChangeViews;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Hetfield.ViewModel
{
    internal class CarsPageVM : EmployeeViewPagesVM<Car>
    {
        private List<Car> allCars;

        private int countToSkip;

        private int allCarsCount;
        public CarsPageVM()
        {

        }

        public RelayCommand NextPageCommand => new RelayCommand(obj => NextPage());
        public RelayCommand PrevPageCommand => new RelayCommand(obj => PrevPage());

        public RelayCommand AboutCarCommand => new RelayCommand(obj => OpenAboutCarView(obj));
        public RelayCommand OpenCarPassportCommand => new RelayCommand(obj => OpenPTSView(obj));

        public RelayCommand ChangeCarCommand => new RelayCommand(obj => OpenChangeView(obj));

        protected override async void Refresh()
        {
            try
            {
                
                ApiClient apiClient = new ApiClient();
                countToSkip = 0;
                var values = await apiClient.GetAllEntityData<Car>();
                allCars = values.ToList();
                allCarsCount = values.Count();
                TableValue = new ObservableCollection<Car>(allCars.Skip(0).Take(3));
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
                countToSkip = 0;
                var values = await apiClient.GetSearchedEntityDataAsync<Car>(searchText);
                allCars = values.ToList();
                allCarsCount = values.Count();
                TableValue = new ObservableCollection<Car>(allCars.Skip(0).Take(3));
            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    new CustomMessageBoxView("Ошибка запроса на сервер").ShowDialog();
                });
            }
        }

        private async void NextPage()
        {
            if (countToSkip + 3 > allCarsCount)
                return;
            else
                countToSkip += 3;
            TableValue = new ObservableCollection<Car>(allCars.Skip(countToSkip).Take(3));
        }

        private async void PrevPage()
        {
            if (countToSkip - 3 < 0)
                return;
            else
                countToSkip -= 3;
            TableValue = new ObservableCollection<Car>(allCars.Skip(countToSkip).Take(3));
        }

        private async void OpenAboutCarView(object parameter)
        {
            if(parameter is int IdCar)
            {
                Car car = allCars.FirstOrDefault(c => c.IdCar == IdCar);
                ContentDialogService.userControlInDialog = new CarMoreInformationUserControlView(car);
                ContentDialogService service = new ContentDialogService();
                service.OpenDialog();
            }
        }

        private async void OpenPTSView(object parameter)
        {
            if (parameter is int IdCar)
            {
                Car car = allCars.FirstOrDefault(c => c.IdCar == IdCar);
                ContentDialogService.userControlInDialog = new PTSUserControlView(car);
                ContentDialogService service = new ContentDialogService();
                service.OpenDialog();
            }
        }

        private async void OpenChangeView(object parameter)
        {
            if(parameter is int IdCar)
            {
                Car car = allCars.FirstOrDefault(c => c.IdCar == IdCar);
                ContentDialogService.userControlInDialog = new CarAddAndChange(car);
                ContentDialogService service = new ContentDialogService();
                service.OpenDialog();
            }
        }

    }
}
