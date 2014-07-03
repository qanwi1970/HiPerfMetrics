using System;

namespace HiPerfMetrics
{
    public class TaskInfo
    {
        private readonly HiPerfTimer _timer;

        /// <summary>
        /// The name of a particular task in the larger process
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The total duration of this task
        /// </summary>
        public double Duration { get; private set; }

        /// <summary>
        /// Field constructor
        /// </summary>
        /// <param name="taskName">the name of the task</param>
        public TaskInfo(string taskName)
        {
            Name = taskName;
            _timer = new HiPerfTimer();
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
            Duration = _timer.Duration;
        }
    }
}
