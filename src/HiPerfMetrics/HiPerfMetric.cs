/*************************************************************************
 * HiPerfMetric.cs
 ************************************************************************/

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
        //Keep a list of tasks being recorded
        private readonly IList<ITimeInfo> _timeList;

        //Name of the timer
        public string MetricName { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="metricName">name of the metric for reporting</param>
        public HiPerfMetric(string metricName)
        {
            _timeList = new List<ITimeInfo>();
            MetricName = metricName;
        }

        /// <summary>
        /// Get the total timer time in seconds
        /// </summary>
        /// <returns>total time in seconds</returns>
        public double GetTotalTimeInSeconds()
        {
            return _timeList.Sum(taskInfo => taskInfo.Duration);
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
            _timeList.Add(taskInfo);
            taskInfo.Start();
        }

        public MetricInfo StartChildMetric(string metricName)
        {
            // Let waiting threads do their work
            Thread.Sleep(0);

            var metricInfo = new MetricInfo(metricName);
            _timeList.Add(metricInfo);

            return metricInfo;
        }

        /// <summary>
        /// Stop the timer
        /// </summary>
        public void Stop()
        {
            _timeList.Last().Stop();
        }

        /// <summary>
        /// Utility method for logging the performance timer results
        /// </summary>
        public string SummaryMessage
        {
            get
            {
                return string.Format("HiPerfMetric '{0}' running time - {1:0.0000} seconds", MetricName,
                                     GetTotalTimeInSeconds());
            }
        }

        /// <summary>
        /// Utility method for putting the task details into a generic IEnumerable
        /// </summary>
        public IEnumerable<ITimeInfo> TimeDetails
        {
            get { return _timeList; }
        }
    }
}