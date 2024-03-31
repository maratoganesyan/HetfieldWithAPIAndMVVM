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
    /// Interaction logic for CarAddAndChange.xaml
    /// </summary>
    public partial class CarAddAndChange : UserControl
    {
        public CarAddAndChange()
        {
            InitializeComponent();
            DataContext = new CarAddAndChangeVM();
        }

        public CarAddAndChange(Car car)
        {
            InitializeComponent();
            DataContext = new CarAddAndChangeVM(car);
        }

        private void LeftPageSwapButton_Click(object sender, RoutedEventArgs e)
        {
            if (PTSGrid.Visibility == Visibility.Visible)
                return;
            CarAboutGrid.Visibility = Visibility.Collapsed;
            PTSGrid.Visibility = Visibility.Visible;
        }

        private void RightPageSwapButton_Click(object sender, RoutedEventArgs e)
        {
            if (CarAboutGrid.Visibility == Visibility.Visible)
                return;
            PTSGrid.Visibility = Visibility.Collapsed;
            CarAboutGrid.Visibility = Visibility.Visible;
        }
    }
}
