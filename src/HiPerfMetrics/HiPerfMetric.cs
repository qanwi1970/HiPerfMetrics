/*************************************************************************
 * HiPerfMetric.cs
 ************************************************************************/

using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private readonly string _metricName;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="metricName">name of the metric for reporting</param>
        public HiPerfMetric(string metricName)
        {
            _taskList = new List<TaskInfo>();
            _metricName = metricName;
        }

        /// <summary>
        /// Get the total timer time in seconds
        /// </summary>
        /// <returns>total time in seconds</returns>
        public double GetTotalTimeInSeconds()
        {
            return _taskList.Sum(taskInfo => taskInfo.Timer.Duration);
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

            var taskInfo = new TaskInfo(taskName, new HiPerfTimer());
            _taskList.Add(taskInfo);
            taskInfo.Timer.Start();
        }

        /// <summary>
        /// Stop the timer
        /// </summary>
        public void Stop()
        {
            _taskList.Last().Timer.Stop();
        }

        /// <summary>
        /// Report out all the task timing information
        /// </summary>
        /// <returns>report information</returns>
        public string Report()
        {
            var sb = new StringBuilder();

            sb.Append(string.Format("HiPerfMetric '{0}' running time - {1} seconds<br/>", _metricName,
                                    GetTotalTimeInSeconds()));

            sb.Append("-----------------------------------------<br/>");
            sb.Append("   ms      %  Task name<br/>");
            sb.Append("-----------------------------------------<br/>");

            foreach (var task in _taskList)
            {
                sb.Append(
                    string.Format("{0,5} {1,6:P0}  {2,-14}", task.Timer.Duration*1000,
                                  (task.Timer.Duration/GetTotalTimeInSeconds()), task.Name) + "<br/>");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Utility method for logging the performance timer results
        /// </summary>
        public string SummaryMessage
        {
            get
            {
                return string.Format("HiPerfMetric '{0}' running time - {1:0.0000} seconds", _metricName,
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