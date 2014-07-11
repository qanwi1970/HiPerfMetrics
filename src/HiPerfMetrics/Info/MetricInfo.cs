using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HiPerfMetrics.Info
{
    public class MetricInfo : TaskInfo
    {
        private readonly HiPerfMetric _metric;

        public MetricInfo()
        {
            _metric = new HiPerfMetric();
        }

        public MetricInfo(string metricName) : base(metricName)
        {
            _metric = new HiPerfMetric(metricName);
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
            Duration = _metric.TotalTimeInSeconds;
            Name = _metric.SummaryMessage;
        }

        public List<TaskInfo> TimeDetails
        {
            get { return _metric.TimeDetails; }
        }
    }
}
