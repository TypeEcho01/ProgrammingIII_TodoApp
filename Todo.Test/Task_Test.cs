using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common;

using Task = Todo.Common.Task;

namespace Todo.Test
{
    public class Task_Test
    {
        [Fact]
        public void Constructor_SetsDescription_WhenNotNull()
        {
            // Arrange
            var description = "Details about the task";

            // Act
            var task = new Task("Test", description);

            // Assert
            Assert.True(task.HasDescription);
            Assert.Equal(description, task.Description);
        }

        [Fact]
        public void Constructor_EmptiesDescription_WhenNullOrWhitespace()
        {
            // Arrange
            string? description = "   ";

            // Act
            var task = new Task("Test", description);

            // Assert
            Assert.False(task.HasDescription);
            Assert.Equal(string.Empty, task.Description);
        }

        [Fact]
        public void Constructor_WithDueDate_SetsIsDueTrue()
        {
            // Arrange
            var date = DateTime.UtcNow.AddDays(1);
            var due = new DueDate(date);

            // Act
            var task = new Task("Test", due);

            // Assert
            Assert.True(task.IsDue);
            Assert.Equal(date, task.DueDate.Date);
        }

        [Fact]
        public void Constructor_NoDueDate_SetsEmptyDueDate()
        {
            // Arrange + Act
            var task = new Task("Test");

            // Assert
            Assert.False(task.IsDue);
            Assert.True(ReferenceEquals(task.DueDate, DueDate.Empty));
        }

        [Fact]
        public void Copy_MakesDeepCopy()
        {
            // Arrange
            var task = new Task("Original", "Desc", new DueDate(DateTime.UtcNow));

            // Act
            var copy = task.Copy();

            // Assert
            Assert.NotSame(task, copy);
            Assert.Equal(task.Name, copy.Name);
            Assert.Equal(task.Description, copy.Description);
            Assert.Equal(task.DueDate.Date, copy.DueDate.Date);
        }

        [Fact]
        public void ToString_ShowsNameOnly_WhenNoDescriptionOrDueDate()
        {
            // Arrange
            var task = new Task("Hello");

            // Act
            var result = task.ToString();

            // Assert
            Assert.Equal("Hello", result);
        }

        [Fact]
        public void ToString_IncludesDescription_WhenPresent()
        {
            // Arrange
            var task = new Task("Hello", "World");

            // Act
            var result = task.ToString();

            // Assert
            Assert.Equal("Hello: World", result);
        }

        [Fact]
        public void ToString_IncludesDueDate_WhenPresent()
        {
            // Arrange
            var date = new DateTime(2025, 1, 1);
            var task = new Task("Hello", new DueDate(date));

            // Act
            var result = task.ToString();

            // Assert
            Assert.Equal("Hello (Due 1/1/2025 12:00:00 AM)", result);
        }

        [Fact]
        public void Empty_Task_IsRecognized()
        {
            // Arrange + Act
            var empty = Task.Empty;

            // Assert
            Assert.True(empty.IsEmpty());
            Assert.True(Task.IsEmpty(empty));
        }

        [Fact]
        public void EditingProperties_ChangesHasNameAndHasDescription()
        {
            // Arrange
            var task = new Task("X");

            // Act
            task.Name = "New Name";
            task.Description = "New Desc";

            // Assert
            Assert.True(task.HasName);
            Assert.True(task.HasDescription);
        }

        [Fact]
        public void JsonConstructor_SetsDefaultsCorrectly()
        {
            // Arrange + Act
            var task = new Task();

            // Assert
            Assert.Equal("Task", task.Name);
            Assert.Equal(string.Empty, task.Description);
            Assert.True(task.IsInProgress);
            Assert.True(ReferenceEquals(task.DueDate, DueDate.Empty));
        }

        [Fact]
        public void State_Transitions_AreCorrect()
        {
            // Arrange
            var task = new Task("X");

            // Act
            task.Complete();

            // Assert
            Assert.True(task.IsComplete);
            Assert.False(task.IsInProgress);
        }
    }
}
