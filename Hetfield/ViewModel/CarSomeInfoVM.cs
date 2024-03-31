using Hetfield.Models;
using Hetfield.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hetfield.Tools.MVVMTools;
using System.Threading.Channels;

namespace Hetfield.ViewModel
{
    internal class CarSomeInfoVM : ViewModelBase
    {
        private Car _someCar;

        public Car SomeCar { get => _someCar; set { _someCar = value; OnPropertyChanged(nameof(SomeCar)); } }

        public CarSomeInfoVM(Car car)
        {
            SomeCar = car;
        }

        public RelayCommand CloseCommand => new RelayCommand(obj => Close());

        public void Close()
        {
            ContentDialogService service = new ContentDialogService();
            service.CloseDialog();
            
        }

        
    }
}
