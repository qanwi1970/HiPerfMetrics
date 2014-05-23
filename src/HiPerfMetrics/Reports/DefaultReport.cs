﻿using System.Text;

namespace HiPerfMetrics.Reports
{
    public class DefaultReport : IMetricReport
    {
        public HiPerfMetric Metric { get; set; }

        public string Report()
        {
            var totalTime = Metric.GetTotalTimeInSeconds();
            var sb = new StringBuilder();

            sb.Append(string.Format("HiPerfMetric '{0}' running time - {1:##.###} seconds\n", Metric.MetricName,
                                    totalTime));

            sb.Append("-----------------------------------------\n");
            sb.Append("   ms      %    Task name\n");
            sb.Append("-----------------------------------------\n");

            foreach (var task in Metric.TaskDetails)
            {
                sb.Append(
                    string.Format("{0:##.000} {1,6:P0}  {2,-14}", task.Timer.Duration * 1000,
                                  (task.Timer.Duration / totalTime), task.Name) + "\n");
            }

            return sb.ToString();
        }
    }
}
