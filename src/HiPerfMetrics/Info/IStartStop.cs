using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiPerfMetrics.Info
{
	public interface IStartStop
	{
		void Start();
		void Start(string taskName);
		void Stop();
		MetricInfo StartChildMetric(string metricName);
	}
}
