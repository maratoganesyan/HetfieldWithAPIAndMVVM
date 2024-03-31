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

namespace Hetfield.ViewModel.AddAndChangeVM
{
    internal class CarAddAndChangeVM : ReferenceInformationAddAndChangeVM<Car>
    {
        #region fields
        private ICollection<ManufactureYear> _years;

        private ICollection<CarColor> _colors;

        private ICollection<CarEngine> _engines;

        private ICollection<CarConfiguration> _configurations;

        private ICollection<CarStatus> _statuses;

        private ICollection<CarTranssmission> _transsmissions;

        private ICollection<CarType> _types;

        private ICollection<User> _owners;

        private ObservableCollection<CarPhoto> _carPhotos;
        #endregion

        #region props
        public ICollection<ManufactureYear> Years { get => _years; set { _years = value; OnPropertyChanged(nameof(Years)); } }
        public ICollection<CarColor> Colors { get => _colors; set { _colors = value; OnPropertyChanged(nameof(Colors)); } }
        public ICollection<CarEngine> Engines { get => _engines; set { _engines = value; OnPropertyChanged(nameof(Engines)); } }
        public ICollection<CarConfiguration> Configurations { get => _configurations; set { _configurations = value; OnPropertyChanged(nameof(Configurations)); } }
        public ICollection<CarStatus> Statuses { get => _statuses; set { _statuses = value; OnPropertyChanged(nameof(Statuses)); } }
        public ICollection<CarTranssmission> Transsmissions { get => _transsmissions; set { _transsmissions = value; OnPropertyChanged(nameof(Transsmissions)); } }
        public ICollection<CarType> Types { get => _types; set { _types = value; OnPropertyChanged(nameof(Types)); } }
        public ICollection<User> Owners { get => _owners; set { _owners = value; OnPropertyChanged(nameof(Owners)); } }

        public ObservableCollection<CarPhoto> CarPhotos { get => _carPhotos; set { _carPhotos = value; OnPropertyChanged(nameof(CarPhotos)); } }
        #endregion

        public CarAddAndChangeVM() : base() 
        {
            FillComboBoxes();
            CarPhotos = new ObservableCollection<CarPhoto>(TableValue.CarPhotos);
            TableValue.IdCarPassportNavigation = new CarsPassport();
            TableValue.IdCarPassportNavigation.DateOfIssue = new DateTime(2000, 1, 1);
        }
        public CarAddAndChangeVM(Car car) : base(car)
        {
            FillComboBoxes();
            CarPhotos = new ObservableCollection<CarPhoto>(TableValue.CarPhotos);

        }

        public RelayCommand AddPhotoCommand => new RelayCommand(obj => AddPhoto());

        public RelayCommand ChangePhotoCommand => new RelayCommand(obj => ChangePhoto(obj));


        private async void FillComboBoxes()
        {
            ApiClient apiClient = new ApiClient();
            var years = await apiClient.GetAllEntityData<ManufactureYear>();
            var colors = await apiClient.GetAllEntityData<CarColor>();
            var engines = await apiClient.GetAllEntityData<CarEngine>();
            var configurations = await apiClient.GetAllEntityData<CarConfiguration>();
            var statuses = await apiClient.GetAllEntityData<CarStatus>();
            var transsmissions = await apiClient.GetAllEntityData<CarTranssmission>();
            var types = await apiClient.GetAllEntityData<CarType>();
            var users = await apiClient.GetAllEntityData<User>();
            var owners = users.Where(u => u.IdRole == AllRoles.Client);
            Years = new ObservableCollection<ManufactureYear>(years);
            Colors = new ObservableCollection<CarColor>(colors);
            Engines = new ObservableCollection<CarEngine>(engines);
            Configurations = new ObservableCollection<CarConfiguration>(configurations);
            Statuses = new ObservableCollection<CarStatus>(statuses);
            Transsmissions = new ObservableCollection<CarTranssmission>(transsmissions);
            Types = new ObservableCollection<CarType>(types);
            Owners = new ObservableCollection<User>(owners);
        }

        private void AddPhoto()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;
                var photo = File.ReadAllBytes(path);
                CarPhoto carPhoto = new CarPhoto()
                {
                    IdPhoto = CarPhotos.Count() == 0 ? 1 : CarPhotos.Max(p => p.IdPhoto) + 1,
                    Photo = photo
                };
                CarPhotos.Add(carPhoto);
            }
        }

        private void ChangePhoto(object parameter)
        {
            if(parameter is int IdPhoto)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";
                if (openFileDialog.ShowDialog() == true)
                {
                    string path = openFileDialog.FileName;
                    var photo = File.ReadAllBytes(path);
                    CarPhoto carPhoto = CarPhotos.FirstOrDefault(p => p.IdPhoto == IdPhoto);
                    carPhoto.Photo = photo;
                    CarPhotos = new ObservableCollection<CarPhoto>(CarPhotos);
                }
            }
        }

        protected override void Add()
        {
            TableValue.CarPhotos = new List<CarPhoto>();
            foreach(var photo in CarPhotos)
                TableValue.CarPhotos.Add(photo);
            base.Add();
        }

        protected override void Change()
        {
            TableValue.CarPhotos = new List<CarPhoto>();
            foreach (var photo in CarPhotos)
                TableValue.CarPhotos.Add(photo);
            base.Change();
        }
    }
}
