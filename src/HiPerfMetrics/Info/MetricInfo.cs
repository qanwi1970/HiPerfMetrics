using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace HiPerfMetrics.Info
{
    public class MetricInfo : TaskInfo
    {
        public MetricInfo()
        {
            TimeDetails = new List<TaskInfo>();
        }

        public MetricInfo(string metricName) : base(metricName)
        {
            TimeDetails = new List<TaskInfo>();
        }

        public List<TaskInfo> TimeDetails { get; set; }

        /// <summary>
        /// Sum of all the steps' durations
        /// </summary>
        public double TotalTimeInSeconds
        {
            get { return TimeDetails.Sum(taskInfo => taskInfo.Duration); }
            set { }
        }

        public override double Duration
        {
            get { return TotalTimeInSeconds; }
            set { }
        }

        /// <summary>
        /// Utility method for logging the performance timer results
        /// </summary>
        public string SummaryMessage
        {
            get
            {
                return string.Format("'{0}': Time = {1:0.0000} seconds", Name,
                    TotalTimeInSeconds);
            }
            set { }
        }

        public override void Start()
        {
            Start("Step " + TimeDetails.Count() + 1);
        }

        public void Start(string taskName)
        {
            // lets do the waiting threads their work
            Thread.Sleep(0);

            var taskInfo = new TaskInfo(taskName);
            TimeDetails.Add(taskInfo);
            taskInfo.Start();
        }

        public override void Stop()
        {
            TimeDetails.Last().Stop();
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
    }
}
