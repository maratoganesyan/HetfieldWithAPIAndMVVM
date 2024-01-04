using APIForHetfield;
using Hetfield.Models;
using Hetfield.Tools;
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


        public ObservableCollection<Role> Roles { get => _roles; set { _roles = value; OnPropertyChanged(nameof(Roles)); } }
        public ObservableCollection<Gender> Genders { get => _genders; set { _genders = value; OnPropertyChanged(nameof(Genders)); } }

        public UsersAddAndChangeVM() :base() 
        {
            FillComboBoxes();
            TableValue.Photo = Properties.Resources.DefaultUserPhotoInBytes;
            TableValue.DateOfBirth = new DateTime(1980, 1, 1);
        }
        public UsersAddAndChangeVM(User user) : base(user)
        {
            FillComboBoxes();
        }


        public RelayCommand ChangeUserPhotoCommand => new RelayCommand(obj => ChangeUserPhoto()); 

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
