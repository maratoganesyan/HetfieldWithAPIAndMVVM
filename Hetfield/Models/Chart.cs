using System.Collections.Generic;
using System.Text;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace Hetfield.Models;

public class Chart : DbModelBase
{
    public (double[] Sales, string[] ModelsNames) SalesStatistic { get; set; }

    public (double[] Sales, string[] ModelsNames) AnnouncementStatistics { get; set; }

    public (double[] Sales, double[] DateTimes) OrderByDate { get; set; }
}
