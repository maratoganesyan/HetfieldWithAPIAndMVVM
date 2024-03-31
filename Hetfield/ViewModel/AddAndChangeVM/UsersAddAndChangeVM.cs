using APIForHetfield;
using Hetfield.Models;
using Hetfield.Tools;
using Hetfield.Tools.DbUtils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Hetfield.ViewModel.AddAndChangeVM
{
    class UsersAddAndChangeVM : ReferenceInformationAddAndChangeVM<User>
    {
        private ObservableCollection<Role> _roles;

        private ObservableCollection<Gender> _genders;

        private bool _isRoleChangeEnabled;


        public ObservableCollection<Role> Roles { get => _roles; set { _roles = value; OnPropertyChanged(nameof(Roles)); } }
        public ObservableCollection<Gender> Genders { get => _genders; set { _genders = value; OnPropertyChanged(nameof(Genders)); } }

        public bool IsRoleChangeEnabled { get => _isRoleChangeEnabled; set { _isRoleChangeEnabled = value; OnPropertyChanged(nameof(IsRoleChangeEnabled)); } }

        
        public UsersAddAndChangeVM() :base() 
        {
            FillComboBoxes();
            IsRoleChangeEnabled = true;
            TableValue.Photo = Properties.Resources.DefaultUserPhotoInBytes;
            TableValue.DateOfBirth = new DateTime(1980, 1, 1);
        }
        public UsersAddAndChangeVM(User user) : base(user)
        {
            IsRoleChangeEnabled = false;
            FillComboBoxes();
        }

        public UsersAddAndChangeVM(bool clientMode) : base()
        {
            TableValue.IdRole = AllRoles.Client;
            TableValue.Photo = Properties.Resources.DefaultUserPhotoInBytes;
            TableValue.DateOfBirth = new DateTime(1980, 1, 1);
            FillNewClient();
        }


        public RelayCommand ChangeUserPhotoCommand => new RelayCommand(obj => ChangeUserPhoto()); 


        private async void FillNewClient()
        {
            await FillComboBoxes();
            TableValue.IdRole = AllRoles.Client;
            IsRoleChangeEnabled = false;
            TableValue.IdRoleNavigation = Roles.Single(r => r.IdRole == AllRoles.Client);
            OnPropertyChanged(nameof(TableValue));
        }
        private void ChangeUserPhoto()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;
                TableValue.Photo = File.ReadAllBytes(path);
                OnPropertyChanged(nameof(TableValue));
            }
        }
        private async Task FillComboBoxes()
        {
            ApiClient apiClient = new ApiClient();
            var roles = await apiClient.GetAllEntityData<Role>();
            Roles = new ObservableCollection<Role>(roles);
            var genders = await apiClient.GetAllEntityData<Gender>();
            Genders = new ObservableCollection<Gender>(genders);
            if (TableValue != null)
                if (TableValue.IdRole != AllRoles.Client)
                    IsRoleChangeEnabled = true;
        }
    }
}
