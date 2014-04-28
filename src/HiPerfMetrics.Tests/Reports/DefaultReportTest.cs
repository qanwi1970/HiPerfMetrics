using System.Diagnostics;
using System.Threading;
using HiPerfMetrics.Reports;
using NUnit.Framework;

namespace HiPerfMetrics.Tests.Reports
{
    [TestFixture]
    public class DefaultReportTest
    {
        [Test]
        public void NormalTwoTaskReport()
        {
            var metric = new HiPerfMetric("NormalTwoTaskReport");
            metric.Start("task 1");
            Thread.Sleep(250);
            metric.Stop();
            metric.Start("task 2");
            Thread.Sleep(500);
            metric.Stop();

            var report = new DefaultReport
            {
                Metric = metric
            };
            Debug.WriteLine(report.Report());
        }
    }
}
