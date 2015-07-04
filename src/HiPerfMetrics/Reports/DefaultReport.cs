using System.Text;

namespace HiPerfMetrics.Reports
{
    public class DefaultReport : IMetricReport
    {
        public HiPerfMetric Metric { get; set; }

        public string Report()
        {
            var totalTime = Metric.TotalTimeInSeconds;
            var sb = new StringBuilder();

            sb.Append(string.Format("HiPerfMetric '{0}' running time - {1:##.###} seconds\n", Metric.MetricName,
                                    totalTime));

            sb.Append("-----------------------------------------\n");
            sb.Append("   ms      %    Task name\n");
            sb.Append("-----------------------------------------\n");

            foreach (var task in Metric.TimeDetails)
            {
                sb.Append(
                    string.Format("{0:##.000} {1,6:P0}  {2,-14}", task.Duration * 1000,
                                  (task.Duration / totalTime), task.Name) + "\n");
            }

            return sb.ToString();
        }
    }

    public static class DefaultReportExtensions
    {
        public static DefaultReport GetDefaultReport(this HiPerfMetric metric)
        {
            return new DefaultReport {Metric = metric};
        }

	    public static string ReportAsDefault(this HiPerfMetric hiPerfMetric)
	    {
		    var report = new DefaultReport {Metric = hiPerfMetric};
		    return report.Report();
	    }
    }
}
