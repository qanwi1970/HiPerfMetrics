/*************************************************************************
 * HiPerfMetric.cs
 ************************************************************************/

using System.Collections.Generic;
using HiPerfMetrics.Info;

namespace HiPerfMetrics
{
    /// <summary>
    /// This is the main class for creating associated timers.
    /// </summary>
    public class HiPerfMetric : IStartStop
    {
        private readonly MetricInfo _metricInfo;

        /// <summary>
        /// The list of tasks and child metrics that make up this metric
        /// </summary>
        public List<TaskInfo> TimeDetails { get { return _metricInfo.TimeDetails; } }

        /// <summary>
        /// Name of the timer
        /// </summary>
        public string MetricName
        {
            get { return _metricInfo.Name; }
            set { }
        }

        /// <summary>
        /// Sum of all the steps' durations
        /// </summary>
        public double TotalTimeInSeconds
        {
            get { return _metricInfo.TotalTimeInSeconds; }
            set { }
        }

        /// <summary>
        /// Utility method for logging the performance timer results
        /// </summary>
        public string SummaryMessage
        {
            get { return _metricInfo.SummaryMessage; }
            set { }
        }

        public HiPerfMetric()
        {
            _metricInfo = new MetricInfo();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="metricName">name of the metric for reporting</param>
        public HiPerfMetric(string metricName)
        {
            _metricInfo = new MetricInfo(metricName);
        }

        /// <summary>
        /// Start the timer
        /// </summary>
        public void Start()
        {
            _metricInfo.Start();
        }

        /// <summary>
        /// Start the timer
        /// </summary>
        /// <param name="taskName">name of task</param>
        public void Start(string taskName)
        {
            _metricInfo.Start(taskName);
        }

        public MetricInfo StartChildMetric(string metricName)
        {
            return _metricInfo.StartChildMetric(metricName);
        }

        /// <summary>
        /// Stop the timer
        /// </summary>
        public void Stop()
        {
            _metricInfo.Stop();
        }

    }
}