using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HiPerfMetrics
{
    public class HiPerfMetric
    {
        //Keep a list of tasks being recorded
        private readonly IList<TaskInfo> _taskList;

        //Name of the timer
        private readonly string _metricName;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="timerName">name of the timer for reporting</param>
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
            double _totalTime = 0;
            foreach (var taskInfo in _taskList)
            {
                _totalTime += taskInfo.Timer.Duration;
            }
            return _totalTime;
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
                    string.Format("{0,5} {1,6:P0}  {2,-14}", task.Timer.Duration * 1000,
                                  (task.Timer.Duration / GetTotalTimeInSeconds()), task.Name) + "<br/>");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Utility method for logging the performance timer results
        /// </summary>
        public string SummaryMessage
        {
            get { return string.Format("HiPerfMetric '{0}' running time - {1:0.0000} seconds", _metricName, GetTotalTimeInSeconds()); }
        }

        /// <summary>
        /// Utility method for putting the task details into AdditionalFields
        /// </summary>
        public IEnumerable<TaskInfo> TaskDetails
        {
            get
            {
                return _taskList;
            }
        }
    }

    public class TaskInfo
    {
        public string Name { get; private set; }
        public HiPerfTimer Timer { get; private set; }

        public TaskInfo(string taskName, HiPerfTimer timer)
        {
            Name = taskName;
            Timer = timer;
        }
    }
}
