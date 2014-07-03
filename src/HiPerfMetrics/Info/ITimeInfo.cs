namespace HiPerfMetrics.Info
{
    public interface ITimeInfo
    {
        /// <summary>
        /// The name of a particular task in the larger process
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The total duration of this task
        /// </summary>
        double Duration { get; }

        void Start();
        void Stop();
    }
}