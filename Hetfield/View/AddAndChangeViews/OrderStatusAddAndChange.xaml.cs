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
using Hetfield.ViewModel;

namespace Hetfield.View.AddAndChangeViews
{
    /// <summary>
    /// Interaction logic for OrderStatusAddAndChange.xaml
    /// </summary>
    public partial class OrderStatusAddAndChange : UserControl
    {
        public OrderStatusAddAndChange()
        {
            InitializeComponent();
            DataContext = new ReferenceInformationAddAndChangeVM<OrderStatus>();
        }

        public OrderStatusAddAndChange(OrderStatus orderStatus)
        {
            InitializeComponent();
            DataContext = new ReferenceInformationAddAndChangeVM<OrderStatus>(orderStatus);
        }
    }
}
