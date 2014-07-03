using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HiPerfMetrics.Info
{
    public class MetricInfo : ITimeInfo
    {
        private readonly HiPerfMetric _metric;

        public MetricInfo(string metricName)
        {
            _metric = new HiPerfMetric(metricName);
        }

        public string Name
        {
            get { return _metric.SummaryMessage; }
        }

        public double Duration
        {
            get { return _metric.GetTotalTimeInSeconds(); }
        }

        public void Start()
        {
            _metric.Start("Step " + _metric.TimeDetails.Count() + 1);
        }

        public void Start(string taskName)
        {
            _metric.Start(taskName);
        }

        public void Stop()
        {
            _metric.Stop();
        }

        public IEnumerable<ITimeInfo> TimeDetails
        {
            get { return _metric.TimeDetails; }
        }
    }
}
