using System.Text;

namespace APIForHetfield.Models
{
    public class Chart
    {
        public (double[] Sales, string[] ModelsNames) SalesStatistic { get; set; }

        public (double[] Sales, string[] ModelsNames) AnnouncementStatistics { get; set; }

        public (double[] Sales, double[] DateTimes) OrderByDate { get; set; }
    }
}
