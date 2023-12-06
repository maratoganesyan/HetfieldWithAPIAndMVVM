using Hetfield.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hetfield.ViewModel
{
    internal class CustomMessageBoxVM : ViewModelBase
    {
        private string _message;

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(Message);
            }
        }

        public CustomMessageBoxVM(Action closeAction, string message)
            : base(closeAction) => Message = message;

        public RelayCommand OkCommand => new RelayCommand(obj => CloseAction.Invoke());
    }
}
