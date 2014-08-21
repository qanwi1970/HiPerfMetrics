using HiPerfMetrics.Info;
using NUnit.Framework;

namespace HiPerfMetrics.Tests.Info
{
    [TestFixture]
    internal class MetricInfoTest
    {
        [Test]
        public void default_ctor_defaults()
        {
            // Arrange

            // Act
            var metricInfo = new MetricInfo();

            // Assert
            Assert.IsNull(metricInfo.Name);
            Assert.IsNotNull(metricInfo.TimeDetails);
            Assert.IsNotNull(metricInfo.SummaryMessage);
            Assert.AreEqual(0.0d, metricInfo.TotalTimeInSeconds);
            Assert.AreEqual(0.0d, metricInfo.Duration);
        }

        [Test]
        public void name_ctor_defaults()
        {
            // Arrange
            const string testName = "Test Name";

            // Act
            var metricInfo = new MetricInfo(testName);

            // Assert
            Assert.AreEqual(testName, metricInfo.Name);
            Assert.IsNotNull(metricInfo.TimeDetails);
            Assert.IsNotNull(metricInfo.SummaryMessage);
            Assert.IsTrue(metricInfo.SummaryMessage.Contains(testName));
            Assert.AreEqual(0.0d, metricInfo.TotalTimeInSeconds);
            Assert.AreEqual(0.0d, metricInfo.Duration);
        }
    }
}
