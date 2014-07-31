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

        [Test]
        public void GrandpaReport()
        {
            // Arrange
            var testMetric = new HiPerfMetric("GrandpaReport");

            // Act
            testMetric.Start("Grandparent task 1");
            Thread.Sleep(31);
            testMetric.Stop();
            var parent1 = testMetric.StartChildMetric("Parent 1 Metric");
            parent1.Start("Parent 1 task 1");
            Thread.Sleep(31);
            parent1.Stop();
            var child1 = parent1.StartChildMetric("Child 1 metric");
            child1.Start("Child 1 task 1");
            Thread.Sleep(31);
            child1.Stop();
            child1.Start("Child 1 task 2");
            Thread.Sleep(31);
            child1.Stop();
            parent1.Start("Parent 1 task 2");
            Thread.Sleep(31);
            parent1.Stop();

            // Report
            testMetric.GetDeepXmlReport().WriteXmlReport(@"E:\logs\GrandpaReportTest.xml");
        }
    }
}
