using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common;

namespace Todo.Test
{
    public sealed class DueDate_Test
    {
        [Fact]
        public void Empty_Defaults_Correct()
        {
            // Arrange + Act
            DueDate empty = DueDate.Empty;

            // Assert
            Assert.True(empty.IsEmpty());
            Assert.Equal(DateTime.MinValue, empty.Date);
        }

        [Fact]
        public void DefaultConstructor_SetsToNow()
        {
            // Arrange
            DateTime before = DateTime.Now;

            // Act
            DueDate due = new DueDate();

            // Assert
            Assert.True(due.Date >= before);
            Assert.True(due.Date <= DateTime.Now);
        }

        [Fact]
        public void ParameterConstructor_SetsCorrectDate()
        {
            // Arrange
            DateTime dt = new DateTime(2025, 1, 1);

            // Act
            DueDate due = new DueDate(dt);

            // Assert
            Assert.Equal(dt, due.Date);
        }

        [Fact]
        public void YearMonthDayConstructor_SetsCorrectDate()
        {
            // Arrange + Act
            DueDate due = new DueDate(2025, 12, 25);

            // Assert
            Assert.Equal(new DateTime(2025, 12, 25), due.Date);
        }

        [Fact]
        public void YearMonthDayHourConstructor_SetsCorrectDate()
        {
            // Arrange + Act
            DueDate due = new DueDate(2025, 12, 25, 14);

            // Assert
            Assert.Equal(new DateTime(2025, 12, 25, 14, 0, 0), due.Date);
        }

        [Fact]
        public void FullConstructor_SetsCorrectDate()
        {
            // Arrange + Act
            DueDate due = new DueDate(2025, 12, 25, 14, 30);

            // Assert
            Assert.Equal(new DateTime(2025, 12, 25, 14, 30, 0), due.Date);
        }

        [Fact]
        public void IsOnTime_Success()
        {
            // Arrange
            DueDate future = new DueDate(DateTime.Now.AddDays(1));

            // Act
            bool futureOnTime = future.IsOnTime();

            // Assert
            Assert.True(futureOnTime);
        }

        [Fact]
        public void IsLate_Success()
        {
            // Arrange
            DueDate past = new DueDate(DateTime.Now.AddMinutes(-5));

            // Act
            bool isLate = past.IsLate();

            // Assert
            Assert.True(isLate);
        }

        [Fact]
        public void ChangeDate_WithDateTime_SetsCorrectDate()
        {
            // Arrange
            DueDate due = new DueDate(DateTime.Now);
            DateTime newDate = new DateTime(2030, 1, 1);

            // Act
            due.ChangeDate(newDate);

            // Assert
            Assert.Equal(newDate, due.Date);
        }

        [Fact]
        public void ChangeDate_WithDueDate_SetsCorrectDate()
        {
            // Arrange
            DueDate due = new DueDate(DateTime.Now);
            DueDate other = new DueDate(new DateTime(2040, 1, 1));

            // Act
            due.ChangeDate(other);

            // Assert
            Assert.Equal(other.Date, due.Date);
        }

        [Fact]
        public void ToString_ReturnsDateString()
        {
            // Arrange
            DateTime dt = new DateTime(2025, 3, 14, 9, 26, 0);
            DueDate due = new DueDate(dt);

            // Act
            string result = due.ToString();

            // Assert
            Assert.Equal(dt.ToString(), result);
        }
    }
}
