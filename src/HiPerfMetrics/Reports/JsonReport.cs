using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HiPerfMetrics.Reports
{
	public class JsonReport : IMetricReport
	{
		public HiPerfMetric Metric { get; set; }

		public string SummaryMessage
		{
			get { return Metric.SummaryMessage; }
		}

		public string StringJsonReport()
		{
			var serializedMetric = JsonConvert.SerializeObject(Metric);
			return serializedMetric;
		}
	}

	public static class JsonReportExtensions
	{
		public static JsonReport GetJsonReport(this HiPerfMetric hiPerfMetric)
		{
			return new JsonReport {Metric = hiPerfMetric};
		}

		public static string ReportAsJson(this HiPerfMetric hiPerfMetric)
		{
			var report = new JsonReport {Metric = hiPerfMetric};
			return report.StringJsonReport();
		}
	}
}
