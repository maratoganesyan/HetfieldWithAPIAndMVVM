using Hetfield.Models;
using Hetfield.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for GenderAddAndChange.xaml
    /// </summary>
    public partial class GenderAddAndChange : UserControl
    {
        public GenderAddAndChange()
        {
            InitializeComponent();
            DataContext = new ReferenceInformationAddAndChangeVM<Gender>();
        }

        public GenderAddAndChange(Gender gender)
        {
            InitializeComponent();
            DataContext = new ReferenceInformationAddAndChangeVM<Gender>(gender);
        }
    }
}
