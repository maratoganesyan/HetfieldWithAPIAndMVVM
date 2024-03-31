using APIForHetfield;
using Hetfield.Models;
using Hetfield.Tools;
using Hetfield.Tools.MVVMTools;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hetfield.View;
using System.Windows;

//не готово

namespace Hetfield.ViewModel
{
    class ChartsVM : ViewModelBase
    {
        private WpfPlot _salesPlot;

        private WpfPlot _announcementPlot;

        private WpfPlot _orderByDatePlot;

        public WpfPlot SalesPlot
        {
            get => _salesPlot;
            set
            {
                _salesPlot = value;
                OnPropertyChanged(nameof(SalesPlot));
            }
        }

        public WpfPlot AnnouncementPlot { get => _announcementPlot; set { _announcementPlot = value; OnPropertyChanged(nameof(AnnouncementPlot)); } }

        public WpfPlot OrderByDatePlot { get => _orderByDatePlot; set { _orderByDatePlot = value; OnPropertyChanged(nameof(OrderByDatePlot)); } }

        public ChartsVM(WpfPlot salesPlot, WpfPlot announcementPlot, WpfPlot orderByDatePlot)
        {
            SalesPlot = salesPlot;
            AnnouncementPlot = announcementPlot;
            OrderByDatePlot = orderByDatePlot;
            InitPlots();
        }



        private async void InitPlots()
        {
            ContentDialogService.userControlInDialog = new LoadingUserControlView("Подождите пожайлуста. Идет генерация графиков");
            ContentDialogService service = new ContentDialogService();
            service.OpenDialog();
            ApiClient apiClient = new ApiClient();
            Chart chart = await apiClient.GetSingleEntityData<Chart>();
            await Task.Run(() =>
            {
                
                SetPlotOrderByDate(chart.OrderByDate);
                SetPie(chart.SalesStatistic, SalesPlot, "Статистика продаж моделей автомобилей Mercedez-Benz.");
                SetPie(chart.AnnouncementStatistics, AnnouncementPlot, "Статистика выставленных моделей автомобилей Mercedez-Benz.");
            });
            service.CloseDialog();
        }

        private void SetPie((double[] Sales, string[] ModelsNames) Stats, WpfPlot plot, string Title)
        {
            for (int i = 0; i < Stats.Sales.Length; i++)
                Stats.ModelsNames[i] += $" ({Stats.Sales[i] / Stats.Sales.Sum() * 100:.0}%)";

            var pie = plot.Plot.AddPie(Stats.Sales);

            pie.SliceLabels = Stats.Sales.Select(x => x.ToString()).ToArray();
            pie.ShowLabels = true;
            pie.LegendLabels = Stats.ModelsNames;

            pie.Explode = true;

            plot.Plot.Legend();
            plot.Plot.Title(Title);

            pie.Size = .75;
            Application.Current.Dispatcher.Invoke(() => plot.Refresh());
        }

        public void SetPlotOrderByDate((double[] Values, double[] DateTimes) data)
        {
            var bar = OrderByDatePlot.Plot.AddBar(values: data.Values, positions: data.DateTimes);
            OrderByDatePlot.Plot.XAxis.DateTimeFormat(true);
            Application.Current.Dispatcher.Invoke(() => OrderByDatePlot.Refresh());
            bar.BarWidth = 1.0 / 2 * .8;
            OrderByDatePlot.Plot.SetAxisLimits(yMin: 0);
            OrderByDatePlot.Plot.XAxis.TickLabelFormat("yyyy\\/MM\\/dd", dateTimeFormat: true);
            OrderByDatePlot.Plot.Layout(right: 20);
            OrderByDatePlot.Plot.Title("Количество проданных автомобилей, относительно первой и последней даты продажи.");
            OrderByDatePlot.Plot.XAxis.Label("Дни (от первой, до последней даты продажи)");
            OrderByDatePlot.Plot.YAxis.Label("Количество проданных автомобилей");
            Application.Current.Dispatcher.Invoke(() => OrderByDatePlot.Refresh());
        }

    }
}
