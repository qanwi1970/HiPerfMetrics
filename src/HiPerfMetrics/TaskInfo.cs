using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiPerfMetrics
{
    public class TaskInfo
    {
        /// <summary>
        /// The name of a particular task in the larger process
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// the HiPerfTimer associated with this task
        /// </summary>
        public HiPerfTimer Timer { get; private set; }

        /// <summary>
        /// Field constructor
        /// </summary>
        /// <param name="taskName">the name of the task</param>
        /// <param name="timer">the timer</param>
        public TaskInfo(string taskName, HiPerfTimer timer)
        {
            Name = taskName;
            Timer = timer;
        }
    }
}
