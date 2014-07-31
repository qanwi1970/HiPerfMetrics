using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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

        public override void Start()
        {
            _metric.Start("Step " + _metric.TimeDetails.Count() + 1);
        }

        public void Start(string taskName)
        {
            _metric.Start(taskName);
        }

        public override void Stop()
        {
            _metric.Stop();
            Duration = _metric.TotalTimeInSeconds;
            Name = _metric.SummaryMessage;
        }

        public MetricInfo StartChildMetric(string metricName)
        {
            // Let waiting threads do their work
            Thread.Sleep(0);

            var metricInfo = new MetricInfo(metricName);
            TimeDetails.Add(metricInfo);

            return metricInfo;
        }

        public void AddChildMetric(MetricInfo childMetricInfo)
        {
            TimeDetails.Add(childMetricInfo);
        }

        public List<TaskInfo> TimeDetails
        {
            get { return _metric.TimeDetails; }
        }
    }
}
