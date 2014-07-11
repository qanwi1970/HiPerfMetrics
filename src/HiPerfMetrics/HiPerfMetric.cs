/*************************************************************************
 * HiPerfMetric.cs
 ************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using HiPerfMetrics.Info;

namespace HiPerfMetrics
{
    /// <summary>
    /// This is the main class for creating associated timers.
    /// </summary>
    public class HiPerfMetric
    {
        /// <summary>
        /// The list of tasks and child metrics that make up this metric
        /// </summary>
        public List<TaskInfo> TimeDetails { get; set; }

        /// <summary>
        /// Name of the timer
        /// </summary>
        public string MetricName { get; set; }

        /// <summary>
        /// Get the total timer time in seconds
        /// </summary>
        /// <returns>total time in seconds</returns>
        [Obsolete("Use new TotalTimeInSeconds property instead.")]
        public double GetTotalTimeInSeconds()
        {
            return TotalTimeInSeconds;
        }

        private double _totalTime;
        /// <summary>
        /// Sum of all the steps' durations
        /// </summary>
        public double TotalTimeInSeconds {
            get
            {
                _totalTime = TimeDetails.Sum(taskInfo => taskInfo.Duration);
                return _totalTime;
            }
            set { _totalTime = value; } }

        /// <summary>
        /// Utility method for logging the performance timer results
        /// </summary>
        public string SummaryMessage { get; set; }

        public HiPerfMetric()
        {
            TimeDetails = new List<TaskInfo>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="metricName">name of the metric for reporting</param>
        public HiPerfMetric(string metricName)
        {
            TimeDetails = new List<TaskInfo>();
            MetricName = metricName;
        }

        /// <summary>
        /// Start the timer
        /// </summary>
        public void Start()
        {
            Start("");
        }

        /// <summary>
        /// Start the timer
        /// </summary>
        /// <param name="taskName">name of task</param>
        public void Start(string taskName)
        {
            // lets do the waiting threads their work
            Thread.Sleep(0);

            var taskInfo = new TaskInfo(taskName);
            TimeDetails.Add(taskInfo);
            taskInfo.Start();
        }

        public MetricInfo StartChildMetric(string metricName)
        {
            // Let waiting threads do their work
            Thread.Sleep(0);

            var metricInfo = new MetricInfo(metricName);
            TimeDetails.Add(metricInfo);

            return metricInfo;
        }

        /// <summary>
        /// Stop the timer
        /// </summary>
        public void Stop()
        {
            TimeDetails.Last().Stop();
            TotalTimeInSeconds = TimeDetails.Sum(taskInfo => taskInfo.Duration);
            SummaryMessage = string.Format("HiPerfMetric '{0}' running time - {1:0.0000} seconds", MetricName,
                TotalTimeInSeconds);
        }
    }
}