using Hetfield.Models;
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
using Hetfield.ViewModel;

namespace Hetfield.View.AddAndChangeViews
{
    /// <summary>
    /// Interaction logic for CarEngineAddAndChange.xaml
    /// </summary>
    public partial class CarEngineAddAndChange : UserControl
    {
        public CarEngineAddAndChange()
        {
            InitializeComponent();
            DataContext = new ReferenceInformationAddAndChangeVM<CarEngine>(); 
        }

        public CarEngineAddAndChange(CarEngine carEngine)
        {
            InitializeComponent();
            DataContext = new ReferenceInformationAddAndChangeVM<CarEngine>(carEngine);
        }
    }
}
