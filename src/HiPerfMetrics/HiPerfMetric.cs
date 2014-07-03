/*************************************************************************
 * HiPerfMetric.cs
 ************************************************************************/

using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace HiPerfMetrics
{
    /// <summary>
    /// This is the main class for creating associated timers.
    /// </summary>
    public class HiPerfMetric
    {
        //Keep a list of tasks being recorded
        private readonly IList<TaskInfo> _taskList;

        //Name of the timer
        public string MetricName { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="metricName">name of the metric for reporting</param>
        public HiPerfMetric(string metricName)
        {
            _taskList = new List<TaskInfo>();
            MetricName = metricName;
        }

        /// <summary>
        /// Get the total timer time in seconds
        /// </summary>
        /// <returns>total time in seconds</returns>
        public double GetTotalTimeInSeconds()
        {
            return _taskList.Sum(taskInfo => taskInfo.Duration);
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
            _taskList.Add(taskInfo);
            taskInfo.Start();
        }

        /// <summary>
        /// Stop the timer
        /// </summary>
        public void Stop()
        {
            _taskList.Last().Stop();
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
        public IEnumerable<TaskInfo> TaskDetails
        {
            get { return _taskList; }
        }
    }
}