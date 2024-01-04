using APIForHetfield;
using Hetfield.Models;
using Hetfield.Tools;
using Hetfield.Tools.DbUtils;
using Hetfield.View;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Hetfield.ViewModel
{
    internal class AuthVM : ViewModelBase
    {
        #region fields
        private User _logInUser;

        private int _checkingNumber1;

        private int _checkingNumber2;

        private bool _saveUserInSystem;

        private string _userAnswerOnChecking;

        private int attemptCount;

        private ContentDialog checkingDialog;

        #endregion

        #region props
        public User LogInUser
        {
            get => _logInUser;
            set
            {
                _logInUser = value;
                OnPropertyChanged(nameof(LogInUser));
            }
        }
        public int CheckingNumber1
        {
            get => _checkingNumber1;
            set
            {
                _checkingNumber1 = value;
                OnPropertyChanged(nameof(CheckingNumber1));
            }
        }
        public int CheckingNumber2
        {
            get => _checkingNumber2;
            set
            {
                _checkingNumber2 = value;
                OnPropertyChanged(nameof(CheckingNumber2));
            }
        }
        public bool SaveUserInSystem
        {
            get => _saveUserInSystem;
            set
            {
                _saveUserInSystem = value;
                OnPropertyChanged(nameof(SaveUserInSystem));
            }
        }
        public string UserAnswerOnChecking
        {
            get => _userAnswerOnChecking;
            set
            {
                _userAnswerOnChecking = value;
                OnPropertyChanged(nameof(UserAnswerOnChecking));
            }
        }

        #endregion

        public AuthVM(Action closeAction, FrameworkElement elementForAnimation, ContentDialog checkingDialog)
            : base(closeAction, elementForAnimation)
        {
            LogInUser = new User();
            attemptCount = 0;
            this.checkingDialog = checkingDialog;
        }

        #region Commands
        public RelayCommand LoadCommand => new RelayCommand(obj =>  Load());
        public RelayCommand CloseCommand => new RelayCommand(obj => CloseAction.Invoke());
        public RelayCommand AuthCommand => new RelayCommand(obj => new Thread(password => Auth(password as string)).Start(obj as string));
        public RelayCommand GetCheckAnswer => new RelayCommand(obj => ValidateCheckingAnswer());
        #endregion

        #region methods
        private void Load()
        {
            StartAnimation(1500);
        }
        private async void Auth(string? password)
        {
            User? user = null; 
            try
            {
                user = await FindUser(password);
            }
            catch(Exception ex)
            {
                Application.Current.Dispatcher.BeginInvoke(() =>
                {
                    new CustomMessageBoxView("Проблема с откликом сервера").ShowDialog();
                });
            }
            if (user != null)
            {
                Application.Current.Dispatcher.BeginInvoke(() =>
                {
                    new EmployeeView(user).Show();
                    CloseAction.Invoke();
                    
                }); 
            }
            else
            {
                attemptCount++;
                Application.Current.Dispatcher.BeginInvoke(() =>
                {
                    
                    if (attemptCount == 3)
                    {
                        GenerateCheckingDialog();
                        checkingDialog.ShowAsync();
                    }
                    else
                        new CustomMessageBoxView("Логин или пароль введены неверно").ShowDialog();
                });
                
            }
        }
        private async Task<User> FindUser(string password)
        {
            try
            {
                ApiClient apiClient = new ApiClient();
                var users = apiClient.GetAllEntityData<User>().Result;
                if (users.Any(u => u.Login == LogInUser.Login &&
                             u.Password == password &&
                             (u.IdRole == AllRoles.Admin || u.IdRole == AllRoles.SalesManager || u.IdRole == AllRoles.Director)))
                {
                    User user = users.Single(u => u.Login == LogInUser.Login && u.Password == password);
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private void GenerateCheckingDialog()
        {
            Random rand = new Random();
            CheckingNumber1 = rand.Next(0, 99);
            CheckingNumber2 = rand.Next(0, 99);
            UserAnswerOnChecking = "";
        }
        private void ValidateCheckingAnswer()
        {
            if ((CheckingNumber1 + CheckingNumber2).ToString() == UserAnswerOnChecking)
            {
                checkingDialog.Hide();
                attemptCount = 0;
            }
            else
                GenerateCheckingDialog();
                
        }
#endregion
    }
}
