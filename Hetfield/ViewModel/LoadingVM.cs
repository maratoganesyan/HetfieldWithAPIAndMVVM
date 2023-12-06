using APIForHetfield;
using Hetfield.Models;
using Hetfield.Tools;
using Hetfield.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Hetfield.ViewModel
{
    internal class LoadingVM : ViewModelBase
    {
        private RelayCommand _loadCommand;
        
        public LoadingVM(Action closeAction, FrameworkElement elementForAnimation)
            : base(closeAction, elementForAnimation)
        {

        }

        public RelayCommand LoadCommand => new RelayCommand(obj => Load());

        private void Load()
        {
            StartAnimation(2000);
            LoadDbData();
        }
        private async void LoadDbData()
        {
            ApiClient apiClient = new ApiClient();
            var users = await apiClient.GetAllEntityData<User>();
            var AuthView = new AuthView();
            AuthView.Show();
            CloseAction.Invoke();
        }
    }
}
