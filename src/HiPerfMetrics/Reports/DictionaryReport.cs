using System;
using System.Collections.Generic;
using System.Linq;

namespace HiPerfMetrics.Reports
{
    public class DictionaryReport : IMetricReport
    {
        public HiPerfMetric Metric { get; set; }

        public string SummaryMessage
        {
            get { return Metric.SummaryMessage; }
        }

        public Dictionary<string, string> TaskDetails
        {
            get
            {
                return Metric.TaskDetails.ToDictionary(taskDetail => taskDetail.Name,
                    taskDetail => String.Format("{0:##.000} ms", (taskDetail.Timer.Duration * 1000.0)));
            }
        }
    }

    public static class DictionaryReportExtensions
    {
        public static DictionaryReport GetDictionaryReport(this HiPerfMetric metric)
        {
            return new DictionaryReport {Metric = metric};
        }
    }
}
