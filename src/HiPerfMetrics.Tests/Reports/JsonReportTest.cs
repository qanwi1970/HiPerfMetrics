using System.Diagnostics;
using System.Threading;
using HiPerfMetrics.Reports;
using NUnit.Framework;

namespace HiPerfMetrics.Tests.Reports
{
	[TestFixture]
	public class JsonReportTest
	{
		[Test]
		public void StringReport()
		{
			var metric = new HiPerfMetric("NormalTwoTaskReport");
			metric.Start("task 1");
			Thread.Sleep(25);
			metric.Stop();
			metric.Start("task 2");
			Thread.Sleep(50);
			metric.Stop();

			Debug.WriteLine(metric.GetJsonReport().StringJsonReport());
		}

		[Test]
		public void ChildrenJson()
		{
			var metric = new HiPerfMetric("TaskWithChildren");
			metric.Start("Parent 1");
			Thread.Sleep(27);
			metric.Stop();
			var parent2 = metric.StartChildMetric("Parent 2");
			parent2.Start("Child 1");
			Thread.Sleep(75);
			parent2.Stop();
			parent2.Start("Child 2");
			Thread.Sleep(50);
			parent2.Stop();
			metric.Start("One last task");
			Thread.Sleep(25);
			metric.Stop();

			Debug.WriteLine(metric.ReportAsJson());
		}
	}
}
