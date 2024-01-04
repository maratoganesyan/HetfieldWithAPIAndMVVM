using Hetfield.Models;
using Hetfield.Tools.DbUtils;
using Hetfield.Tools.MVVMTools;
using Hetfield.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hetfield.View
{
    /// <summary>
    /// Interaction logic for EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : Window
    {
        public EmployeeView(User employee)
        {
            InitializeComponent();
            ContentDialogService.dialog = DialogForAddAndChangePages;
            DataContext = new EmployeeVM(Close, employee);
            SetAccess(employee);

        }

        public void SetAccess(User employee)
        {
            Spr.Visibility = Visibility.Visible;
            UsersMI.Visibility = Visibility.Visible;
            CarsMI.Visibility = Visibility.Visible;
            ChartMI.Visibility = Visibility.Visible;
            ClientsMI.Visibility = Visibility.Visible;
            DocumentsMI.Visibility = Visibility.Visible;
            if (employee.IdRoleNavigation.IdRole == AllRoles.Admin)
            {
                ClientsMI.Visibility = Visibility.Collapsed;
                return;
            }
            if (employee.IdRoleNavigation.IdRole == AllRoles.SalesManager)
            {
                UsersMI.Visibility = Visibility.Collapsed;
                ChartMI.Visibility = Visibility.Collapsed;
                Spr.Visibility = Visibility.Collapsed;
                DocumentsMI.Visibility = Visibility.Collapsed;
                return;
            }
            if (employee.IdRoleNavigation.IdRole == AllRoles.Director)
            {
                Spr.Visibility = Visibility.Collapsed;
                UsersMI.Visibility = Visibility.Collapsed;
                CarsMI.Visibility = Visibility.Collapsed;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Minim_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;


    }
}
