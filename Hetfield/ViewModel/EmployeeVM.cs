using Hetfield.Models;
using Hetfield.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Hetfield.View.Pages;
using System.Reflection.Metadata;
using System.Windows;
using Hetfield.Tools.DbUtils;
using Hetfield.View;

namespace Hetfield.ViewModel
{
    internal class EmployeeVM : ViewModelBase
    {
        #region fields

        private User _employee;

        private Page _currentPage;

        #endregion

        #region props

        public User Employee { get => _employee; set { _employee = value; OnPropertyChanged(nameof(Employee));} }

        public Page CurrentPage{ get => _currentPage; set { _currentPage = value; OnPropertyChanged(nameof(CurrentPage)); } }

        #endregion

        public EmployeeVM(Action closeAction, User employee) : base(closeAction)
        {
            base.CloseAction = closeAction;
            Employee = employee;
        }

        #region Commands

        public RelayCommand CloseCommand => new RelayCommand(obj => CloseAction.Invoke());

        public RelayCommand OpenPageCommand => new RelayCommand(obj => OpenPage(obj));

        public RelayCommand ExitFromAccountCommand => new RelayCommand(obj => ExitFromAccount());

        #endregion

        #region Methods

        private void OpenPage(object parameter)
        {
            if (parameter is Type pageType && typeof(Page).IsAssignableFrom(pageType))
            {
                try
                {
                    CurrentPage = (Page)Activator.CreateInstance(pageType);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при открытии страницы: {ex.Message}");
                }
            }
        }

        private void ExitFromAccount()
        {
            new AuthView().Show();
            CloseAction.Invoke();
        }

        #endregion
    }
}
