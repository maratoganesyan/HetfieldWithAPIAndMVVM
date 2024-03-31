using APIForHetfield;
using Hetfield.Models;
using Hetfield.Tools.DbUtils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Hetfield.ViewModel.AddAndChangeVM
{
    internal class OrderAddAndChangeVM : ReferenceInformationAddAndChangeVM<Order>
    {
        private List<Car> allCars;

        private ICollection<Car> _cars;

        private ICollection<User> _employee;

        private ICollection<User> _sellers;

        private ICollection<User> _clients;

        private ICollection<OrderStatus> _orderStatuses;

        private User _selectedSeller;

        public ICollection<Car> Cars { get => _cars; set { _cars = value; OnPropertyChanged(nameof(Cars)); } }
        public ICollection<User> Employee { get => _employee; set { _employee = value; OnPropertyChanged(nameof(Employee)); } }
        public ICollection<User> Sellers { get => _sellers; set { _sellers = value; OnPropertyChanged(nameof(Sellers)); } }
        public ICollection<User> Clients { get => _clients; set { _clients = value;  OnPropertyChanged(nameof(Clients)); } }
        public ICollection<OrderStatus> OrderStatuses { get => _orderStatuses; set { _orderStatuses = value; OnPropertyChanged(nameof(OrderStatuses)); } }

        public User SelectedSeller { get => _selectedSeller; set { _selectedSeller = value; ChangeCarsBySeller(); OnPropertyChanged(nameof(SelectedSeller)); } }
        public OrderAddAndChangeVM() : base() 
        {
            FillComboBoxes();
            TableValue.DateOfOrder = DateTime.Now;
        }

        public OrderAddAndChangeVM(Order order) :base(order)
        {
            ChangeModeData();
            
        }

        private async Task<bool> FillComboBoxes()
        {
            ApiClient apiClient = new ApiClient();
            Clients = (await apiClient.GetAllEntityData<User>()).ToList().Where(p => p.IdRole == AllRoles.Client).ToList();
            OrderStatuses = (await apiClient.GetAllEntityData<OrderStatus>()).ToList();
            Employee = (await apiClient.GetAllEntityData<User>()).ToList().Where(e => e.IdRole == AllRoles.SalesManager).ToList();
            allCars = (await apiClient.GetAllEntityData<Car>()).ToList().Where(c => c.IdCarStatus == CarStatuses.Exposed).ToList();
            Sellers = (await apiClient.GetAllEntityData<User>()).ToList().Where(u => allCars.Any(c => c.IdCarPassportNavigation.IdOwner == u.IdUser)).ToList();
            return true;
            
        }

        private async void ChangeModeData()
        {
            if(TableValue != null)
            {
                await FillComboBoxes();
                if(!allCars.Any(c => c.IdCar == TableValue.IdCarNavigation.IdCar))
                {
                    if (!allCars.Any(c => c.IdCarPassportNavigation.IdOwner == TableValue.IdCarNavigation.IdCarPassportNavigation.IdOwner))
                        Sellers.Add(TableValue.IdCarNavigation.IdCarPassportNavigation.IdOwnerNavigation);
                    allCars.Add(TableValue.IdCarNavigation);
                    SelectedSeller = TableValue.IdCarNavigation.IdCarPassportNavigation.IdOwnerNavigation;

                }
            }
        }

        private void ChangeCarsBySeller()
        {
            if (SelectedSeller == null)
                SelectedSeller = Sellers.First();
            if (allCars == null)
                return;
            if(SelectedSeller != null)
            {
                Cars = allCars.ToList().Where(p => p.IdCarPassportNavigation.IdOwner == SelectedSeller.IdUser).ToList();
                if(TableValue.IdCarNavigation == null)
                {
                    TableValue.IdCarNavigation = Cars.First();
                    //OnPropertyChanged(nameof(TableValue));
                }

            }
               
        }
    }
}
