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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hetfield.Models;
using Hetfield.ViewModel.AddAndChangeVM;

namespace Hetfield.View.AddAndChangeViews
{
    /// <summary>
    /// Interaction logic for UserAddAndChange.xaml
    /// </summary>
    public partial class UserAddAndChange : UserControl
    {
        public UserAddAndChange()
        {
            InitializeComponent();
            DataContext = new UsersAddAndChangeVM();
        }

        public UserAddAndChange(User user)
        {
            InitializeComponent();
            DataContext = new UsersAddAndChangeVM(user);
        }

        public UserAddAndChange(bool clientMode)
        {
            InitializeComponent();
            DataContext = new UsersAddAndChangeVM(clientMode);
        }
    }
}
