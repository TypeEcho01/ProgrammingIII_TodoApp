using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common;
using Todo.App.ViewModels;
using Xunit;

using Task = Todo.Common.Task;

namespace Todo.Test
{
    public sealed class TaskViewModel_Test
    {
        [Fact]
        public void Changing_Name_InvokesSave()
        {
            // Arrange
            bool saved = false;
            Task task = new Task("Old Name");
            var vm = new TaskViewModel(task, () => saved = true);

            // Act
            vm.Name = "New Name";

            // Assert
            Assert.Equal("New Name", vm.Name);
            Assert.True(saved);
        }

        [Fact]
        public void Changing_Description_InvokesSave()
        {
            // Arrange
            bool saved = false;
            Task task = new Task("Task", "Old Desc");
            var vm = new TaskViewModel(task, () => saved = true);

            // Act
            vm.Description = "New Desc";

            // Assert
            Assert.Equal("New Desc", vm.Description);
            Assert.True(saved);
        }

        [Fact]
        public void Changing_DueDate_InvokesSaveAndUpdatesDerivedProperties()
        {
            // Arrange
            bool saved = false;
            Task task = new Task("Task");
            var vm = new TaskViewModel(task, () => saved = true);

            DateTime dt = DateTime.UtcNow.AddDays(1);
            DueDate newDue = new DueDate(dt);

            // Act
            vm.DueDate = newDue;

            // Assert
            Assert.Equal(newDue, vm.DueDate);
            Assert.Equal($"Due {dt:D} at {dt:t}", vm.DueDateString);
            Assert.True(saved);
        }

        [Fact]
        public void ToggleCompletion_SwitchesStateAndInvokesSave()
        {
            // Arrange
            bool saved = false;
            Task task = new Task("Task");
            var vm = new TaskViewModel(task, () => saved = true);

            // Act
            vm.ToggleCompletion.Execute(null);

            // Assert
            Assert.True(task.IsComplete);
            Assert.Equal("Mark As Incomplete", vm.ToggleCompletionString);
            Assert.True(saved);

            // Act again (toggle back)
            saved = false;
            vm.ToggleCompletion.Execute(null);

            // Assert
            Assert.True(task.IsInProgress);
            Assert.Equal("Mark As Complete", vm.ToggleCompletionString);
            Assert.True(saved);
        }

        [Fact]
        public void State_PropertyChange_InvokesSave()
        {
            // Arrange
            bool saved = false;
            Task task = new Task("Task");
            var vm = new TaskViewModel(task, () => saved = true);

            // Act
            vm.State = TaskState.Complete;

            // Assert
            Assert.Equal(TaskState.Complete, task.State);
            Assert.True(saved);
        }

        [Fact]
        public void Empty_TaskViewModel_HasEmptyTask()
        {
            // Arrange / Act
            var vm = TaskViewModel.Empty;

            // Assert
            Assert.True(vm.Task.IsEmpty());
        }
    }
}
