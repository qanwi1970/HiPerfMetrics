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
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        /// <summary>
        /// Determine if a timer is running
        /// </summary>
        public bool Running { get; private set; }

        //Save off when a timer was started
        private long _startTime;

        //Keep total time
        private long _totalTime;

        //Result from QueryPerformanceFrequency
        private readonly long _frequency;

        //Name of the timer
        private readonly string _timerName;

        //Keep a list of tasks being recorded
        private readonly IList<TaskInfo> _taskList;

        //Keep the current task name for saving to _taskList
        private string _currentTaskName;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="timerName">name of the timer for reporting</param>
        public HiPerfMetric(string timerName)
        {
            _startTime = 0;
            _totalTime = 0;

            if (QueryPerformanceFrequency(out _frequency) == false)
            {
                // high-performance counter not supported
                throw new Win32Exception("High performance counter not supported.");
            }

            _taskList = new List<TaskInfo>();
            _timerName = timerName;
        }

        /// <summary>
        /// Get the total timer time in seconds
        /// </summary>
        /// <returns>total time in seconds</returns>
        public double GetTotalTimeInSeconds()
        {
            return _totalTime / (double)_frequency;
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

            if (Running)
            {
                throw new Exception("Can't start HiPerfTimer:  it's already running");
            }

            QueryPerformanceCounter(out _startTime);

            Running = true;
            _currentTaskName = taskName;
        }

        /// <summary>
        /// Stop the timer
        /// </summary>
        public void Stop()
        {
            long stopTime;

            if (!Running)
            {
                throw new Exception("Can't stop HiPerfTimer:  it's not running");
            }

            QueryPerformanceCounter(out stopTime);

            long lastTime = stopTime - _startTime;

            _totalTime += lastTime;

            _taskList.Add(new TaskInfo(_currentTaskName, (lastTime * 1000) / _frequency));

            Running = false;
            _currentTaskName = string.Empty;
        }

        /// <summary>
        /// Report out all the task timing information
        /// </summary>
        /// <returns>report information</returns>
        public string Report()
        {
            var sb = new StringBuilder();

            sb.Append(string.Format("HiPerfMetric '{0}' running time - {1} seconds<br/>", _timerName,
                                    GetTotalTimeInSeconds()));

            sb.Append("-----------------------------------------<br/>");
            sb.Append("   ms      %  Task name<br/>");
            sb.Append("-----------------------------------------<br/>");

            foreach (var task in _taskList)
            {
                sb.Append(
                    string.Format("{0,5} {1,6:P0}  {2,-14}", task.TimeInMilli,
                                  (task.TimeInMilli / GetTotalTimeInSeconds() / 1000), task.Name) + "<br/>");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Utility method for logging the performance timer results
        /// </summary>
        public string SummaryMessage
        {
            get { return string.Format("HiPerfMetric '{0}' running time - {1:0.0000} seconds", _timerName, GetTotalTimeInSeconds()); }
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
        public long TimeInMilli { get; private set; }

        public TaskInfo(string taskName, long timeMilli)
        {
            Name = taskName;
            TimeInMilli = timeMilli;
        }
    }
}