using Hetfield.Models;
using Hetfield.ViewModel.AddAndChangeVM;
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

namespace Hetfield.View.AddAndChangeViews
{
    /// <summary>
    /// Interaction logic for OrderAddAndChange.xaml
    /// </summary>
    public partial class OrderAddAndChange : UserControl
    {
        public OrderAddAndChange()
        {
            InitializeComponent();
            DataContext = new OrderAddAndChangeVM();
        }
        public OrderAddAndChange(Order order)
        {
            InitializeComponent();
            DataContext = new OrderAddAndChangeVM(order);
        }
    }
}
