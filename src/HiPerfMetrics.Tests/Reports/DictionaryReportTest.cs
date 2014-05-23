using System.Diagnostics;
using System.Threading;
using HiPerfMetrics.Reports;
using NUnit.Framework;

namespace HiPerfMetrics.Tests.Reports
{
    [TestFixture]
    public class DictionaryReportTest
    {
        [Test]
        public void HappyTest()
        {
            var metric = new HiPerfMetric("NormalTwoTaskReport");
            metric.Start("task 1");
            Thread.Sleep(250);
            metric.Stop();
            metric.Start("task 2");
            Thread.Sleep(500);
            metric.Stop();

            var report = new DictionaryReport
            {
                Metric = metric
            };
            Debug.WriteLine(report.SummaryMessage);
            foreach (var taskDetail in report.TaskDetails)
            {
                Debug.WriteLine("{0}\t{1}", taskDetail.Key, taskDetail.Value);
            }
        }
    }
}
