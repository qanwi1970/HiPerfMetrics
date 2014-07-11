using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HiPerfMetrics.Reports;
using NUnit.Framework;

namespace HiPerfMetrics.Tests.Reports
{
    [TestFixture]
    public class DeepXmlReportTest
    {
        [Test]
        public void NormalTwoTaskreport()
        {
            var metric = new HiPerfMetric("NormalTwoTaskReport");
            metric.Start("task 1");
            Thread.Sleep(25);
            metric.Stop();
            metric.Start("task 2");
            Thread.Sleep(50);
            metric.Stop();

            metric.GetDeepXmlReport().WriteXmlReport(@"E:\logs\NormalTwoTaskReport.xml");
        }

        [Test]
        public void ChildReportTest()
        {
            // Arrange
            var testMetric = new HiPerfMetric("ParentMetric");

            // Act
            testMetric.Start("Parent task 1");
            Thread.Sleep(31);
            testMetric.Stop();
            var child = testMetric.StartChildMetric("ChildMetric");
            child.Start("child task 1");
            Thread.Sleep(31);
            child.Stop();
            child.Start("child task 2");
            Thread.Sleep(31);
            child.Stop();
            testMetric.Start("Parent task 2");
            Thread.Sleep(31);
            testMetric.Stop();

            // Report
            testMetric.GetDeepXmlReport().WriteXmlReport(@"E:\logs\ChildReportTest.xml");
        }

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

            Debug.WriteLine(metric.GetDeepXmlReport().StringXmlReport());
        }
    }
}
