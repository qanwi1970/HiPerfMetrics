using System.Threading;
using HiPerfMetrics.Info;
using NUnit.Framework;

namespace HiPerfMetrics.Tests.Info
{
    [TestFixture]
    internal class TaskInfoTest
    {
        [Test]
        public void default_ctor_success()
        {
            // Arrange

            // Act
            var taskInfo = new TaskInfo();

            // Assert
            Assert.IsNull(taskInfo.Name);
            Assert.AreEqual(0.0d, taskInfo.Duration);
        }

        [Test]
        public void name_ctor_success()
        {
            // Arrange
            const string TEST_NAME = "Test Name";

            // Act
            var taskInfo = new TaskInfo(TEST_NAME);

            // Assert
            Assert.AreEqual(TEST_NAME, taskInfo.Name);
            Assert.AreEqual(0.0d, taskInfo.Duration);
        }

        [Test]
        public void start_stop_check_time_success()
        {
            // Arrange

            // Act
            var taskInfo = new TaskInfo();
            taskInfo.Start();
            Thread.Sleep(31);
            taskInfo.Stop();

            // Assert
            Assert.LessOrEqual(.030, taskInfo.Duration);
        }

        [Test]
        public void double_start_no_exception()
        {
            // Arrange

            // Act
            var taskInfo = new TaskInfo();
            taskInfo.Start();
            taskInfo.Start();

            // Assert
            // If we get here w/o an exception, we passed
        }

        [Test]
        public void double_start_restarts_duration()
        {
            // Arrange

            // Act
            var taskInfo = new TaskInfo();
            taskInfo.Start();
            Thread.Sleep(31);
            taskInfo.Start();
            Thread.Sleep(31);
            taskInfo.Stop();

            // Assert
            Assert.LessOrEqual(.030, taskInfo.Duration);
            Assert.GreaterOrEqual(.050, taskInfo.Duration);
        }

        [Test]
        public void double_stop_no_exception()
        {
            // Arrange

            // Act
            var taskInfo = new TaskInfo();
            taskInfo.Start();
            taskInfo.Stop();
            taskInfo.Stop();

            // Assert
            // If we got here w/o an exception, we passed
        }

        [Test]
        public void double_stop_extends_duration()
        {
            // Arrange

            // Act
            var taskInfo = new TaskInfo();
            taskInfo.Start();
            Thread.Sleep(31);
            taskInfo.Stop();
            Thread.Sleep(31);
            taskInfo.Stop();

            // Assert
            Assert.LessOrEqual(.060, taskInfo.Duration);
        }
    }
}
