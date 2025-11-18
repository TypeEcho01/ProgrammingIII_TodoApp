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
        public void Empty_Defaults_Correct()
        {
            Assert.False(Task.Empty.HasName);
            Assert.False(Task.Empty.HasDescription);
            Assert.False(Task.Empty.IsDue);
            Assert.False(Task.Empty.IsComplete);
        }

        [Fact]
        public void Task_Defaults_Correct()
        {
            Task task = new Task("Test Task");

            Assert.Equal("Test Task", task.Name);
            Assert.False(task.HasDescription);
            Assert.False(task.IsDue);
            Assert.False(task.IsComplete);
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
