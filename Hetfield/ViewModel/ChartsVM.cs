using APIForHetfield;
using Hetfield.Tools;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//не готово

namespace Hetfield.ViewModel
{
    class ChartsVM : ViewModelBase
    {
        private WpfPlot _salesPlot;

        public WpfPlot SalesPlot
        {
            get => _salesPlot;
            set
            {
                _salesPlot = value;
                OnPropertyChanged(nameof(SalesPlot));
            }
        }

        public ChartsVM()
        {
            InitPlots();
        }



        private async void InitPlots()
        {
            new Thread(SalesPlotGenerate).Start();
        }

        private void SalesPlotGenerate()
        {
            ApiClient apiClient = new ApiClient();
        }

    }
}
