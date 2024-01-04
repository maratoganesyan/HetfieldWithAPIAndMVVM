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
    /// Interaction logic for CarStatusAddAndChange.xaml
    /// </summary>
    public partial class CarStatusAddAndChange : UserControl
    {
        public CarStatusAddAndChange()
        {
            InitializeComponent();
            DataContext = new ReferenceInformationAddAndChangeVM<CarStatus>(); 
        }

        public CarStatusAddAndChange(CarStatus carStatus)
        {
            InitializeComponent();
            DataContext = new ReferenceInformationAddAndChangeVM<CarStatus>(carStatus);
        }
    }
}
