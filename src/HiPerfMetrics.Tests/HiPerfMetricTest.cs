using System.Diagnostics;
using System.Linq;
using System.Threading;
using HiPerfMetrics.Info;
using NUnit.Framework;

namespace HiPerfMetrics.Tests
{
    [TestFixture]
    public class HiPerfMetricTest
    {
        [Test]
        public void OverallTestHappyPath()
        {
            // Arrange
            var testMetric = new HiPerfMetric("Test");

            // Act
            testMetric.Start("task 1");
            Thread.Sleep(500);
            testMetric.Stop();
            testMetric.Start("task 2");
            testMetric.Stop();

            // Assert
            Debug.WriteLine(testMetric.SummaryMessage);
            Assert.AreEqual(2, testMetric.TimeDetails.Count());
        }

        [Test]
        public void DurationTest()
        {
            // Arrange
            var testMetric = new HiPerfMetric("Test");
            var totalTime = 0.0;

            // Act
            testMetric.Start("task 1");
            Thread.Sleep(501);
            totalTime += .5;
            testMetric.Stop();
            testMetric.Start("task 2");
            Thread.Sleep(1);
            totalTime += 0.0;
            testMetric.Stop();
            testMetric.Start("task 3");
            Thread.Sleep(251);
            totalTime += .25;
            testMetric.Stop();

            // Assert
            Assert.GreaterOrEqual(testMetric.GetTotalTimeInSeconds(), totalTime);
        }

        [Test]
        public void DoubleStart()
        {
            // Arrange
            var testMetric = new HiPerfMetric("test");

            // Act
            testMetric.Start("task 1");
            testMetric.Start("task 2");

            // Assert
            // hopefully there wasn't an exception
        }

        [Test]
        public void DoubleStop()
        {
            // Arrange
            var testMetric = new HiPerfMetric("test");

            // Act
            testMetric.Start("task 1");
            testMetric.Stop();
            testMetric.Stop();

            // Assert
            // hopefully there wasn't an exception
        }

        [Test]
        public void NamelessTasks()
        {
            // Arrange
            var testMetric = new HiPerfMetric("NamelessTasks");

            // Act
            testMetric.Start();
            testMetric.Stop();

            // Assert
            Assert.AreEqual(1, testMetric.TimeDetails.Count());
            Assert.AreEqual("", ((TaskInfo)testMetric.TimeDetails.First()).Name);
        }

        [Test]
        public void ChildMetrics()
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

            // Assert
            Assert.AreEqual(3, testMetric.TimeDetails.Count());
            Assert.GreaterOrEqual(testMetric.TotalTimeInSeconds, .120);
            Assert.AreEqual(2, child.TimeDetails.Count());
            Assert.GreaterOrEqual(child.Duration, .06);
        }
    }
}
