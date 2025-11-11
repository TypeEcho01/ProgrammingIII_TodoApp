using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Todo.Common.Classes;
using Task = Todo.Common.Task;
using Todo.Common;

namespace Todo.Test
{
    public class Task_Test
    {
        [Fact]
        public void Empty_Defaults_Correct()
        {
            Assert.Equal(string.Empty, Task.Empty.Name);
            Assert.Null(Task.Empty.Description);
            Assert.Null(Task.Empty.DueDate);
            Assert.Equal(TaskState.InProgress, Task.Empty.State);
        }

        [Fact]
        public void Task_Defaults_Correct()
        {
            Task task = new Task("Test Task");

            Assert.Equal("Test Task", task.Name);
            Assert.Null(task.Description);
            Assert.Null(task.DueDate);
            Assert.Equal(TaskState.InProgress, task.State);
        }

        [Fact]
        public void IsComplete_Success()
        {
            Task task = new Task("Test Task");

            task.Complete();

            Assert.True(task.IsComplete);
        }

        [Fact]
        public void IsInProgress_Success()
        {
            Task task = new Task("Test Task");

            Assert.True(task.IsInProgress);
        }

        [Fact]
        public void IsDue_Success()
        {
            Task a = new Task("Test Task A");
            Task b = new Task("Test Task B", new DueDate(DateTime.UtcNow));

            Assert.False(a.IsDue);
            Assert.True(b.IsDue);
        }

        [Fact]
        public void Complete_SetsStateToComplete()
        {
            Task task = new Task("Test Task");

            task.Complete();

            Assert.True(task.IsComplete);
            Assert.False(task.IsInProgress);
        }
    }
}
