using System.Threading;
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
            const string TEST_NAME = "Test Name";

            // Act
            var metricInfo = new MetricInfo(TEST_NAME);

            // Assert
            Assert.AreEqual(TEST_NAME, metricInfo.Name);
            Assert.IsNotNull(metricInfo.TimeDetails);
            Assert.IsNotNull(metricInfo.SummaryMessage);
            Assert.IsTrue(metricInfo.SummaryMessage.Contains(TEST_NAME));
            Assert.AreEqual(0.0d, metricInfo.TotalTimeInSeconds);
            Assert.AreEqual(0.0d, metricInfo.Duration);
        }

        [Test]
        public void TotalTimeInSeconds_adds_up()
        {
            // Arrange
            var task1 = new TaskInfo("task1") {Duration = 13};
            var task2 = new TaskInfo("task2") {Duration = 27};

            // Act
            var metricInfo = new MetricInfo();
            metricInfo.TimeDetails.AddRange(new[] {task1, task2});

            // Assert
            Assert.AreEqual(40.0d, metricInfo.TotalTimeInSeconds);
            Assert.AreEqual(40.0d, metricInfo.Duration);
        }

        [Test]
        public void SummaryMessage_assembled()
        {
            // Arrange
            var task1 = new TaskInfo("task1") { Duration = 13 };
            var task2 = new TaskInfo("task2") { Duration = 27 };

            // Act
            var metricInfo = new MetricInfo("TestMetric");
            metricInfo.TimeDetails.AddRange(new[] { task1, task2 });

            // Assert
            Assert.AreEqual("'TestMetric': Time = 40.0000 seconds", metricInfo.SummaryMessage);
        }

        [Test]
        public void Start_adds_task()
        {
            // Arrange
            const string RANDOM_TASK_NAME = "lksdjfsidfuslufsdif";

            // Act
            var metricInfo = new MetricInfo();
            metricInfo.Start(RANDOM_TASK_NAME);

            // Assert
            Assert.AreEqual(1, metricInfo.TimeDetails.Count);
            Assert.AreEqual(RANDOM_TASK_NAME, metricInfo.TimeDetails[0].Name);
        }

        [Test]
        public void Start_Stop_creates_one_task_with_duration()
        {
            // Arrange
            const string RANDOM_TASK_NAME = "cxiuvbyvcxiubt";

            // Act
            var metricInfo = new MetricInfo();
            metricInfo.Start(RANDOM_TASK_NAME);
            Thread.Sleep(31);
            metricInfo.Stop();

            // Assert
            Assert.AreEqual(1, metricInfo.TimeDetails.Count);
            Assert.AreEqual(RANDOM_TASK_NAME, metricInfo.TimeDetails[0].Name);
            Assert.LessOrEqual(.030, metricInfo.TimeDetails[0].Duration);
        }
    }
}
