using APIForHetfield;
using Hetfield.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hetfield.ViewModel.AddAndChangeVM
{
    class UsersAddAndChangeVM : ReferenceInformationAddAndChangeVM<User>
    {
        private ObservableCollection<Role> _roles;

        private ObservableCollection<Gender> _genders;


        public ObservableCollection<Role> Roles { get => _roles; set { _roles = value; OnPropertyChanged(nameof(Roles)); } }
        public ObservableCollection<Gender> Genders { get => _genders; set { _genders = value; OnPropertyChanged(nameof(Genders)); } }

        public UsersAddAndChangeVM() :base() 
        {
            FillComboBoxes();
            TableValue.Photo = Properties.Resources.DefaultUserPhotoInBytes;
        }
        public UsersAddAndChangeVM(User user) : base(user)
        {
            FillComboBoxes();
        }

        private async void FillComboBoxes()
        {
            ApiClient apiClient = new ApiClient();
            var roles = await apiClient.GetAllEntityData<Role>();
            Roles = new ObservableCollection<Role>(roles);
            var genders = await apiClient.GetAllEntityData<Gender>();
            Genders = new ObservableCollection<Gender>(genders);
        }
    }
}
