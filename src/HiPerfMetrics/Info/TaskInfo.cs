using System.Xml.Serialization;

namespace HiPerfMetrics.Info
{
    [XmlInclude(typeof(MetricInfo))]
    public class TaskInfo
    {
        private readonly HiPerfTimer _timer;

        /// <summary>
        /// The name of a particular task in the larger process
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The total duration of this task
        /// </summary>
        public double Duration { get; set; }

        /// <summary>
        /// Field constructor
        /// </summary>
        /// <param name="taskName">the name of the task</param>
        public TaskInfo(string taskName)
        {
            Name = taskName;
            _timer = new HiPerfTimer();
        }

        public TaskInfo()
        {
            _timer = new HiPerfTimer();
        }

        public virtual void Start()
        {
            _timer.Start();
        }

        public virtual void Stop()
        {
            _timer.Stop();
            Duration = _timer.Duration;
        }
    }
}
