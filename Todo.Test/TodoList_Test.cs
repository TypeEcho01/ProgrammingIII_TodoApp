using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Todo.Common;

using Task = Todo.Common.Task;

namespace Todo.Test
{
    public class TodoList_Test
    {
        [Fact]
        public void Empty_Defaults_Correct()
        {
            Assert.False(TodoList.Empty.HasName);
            Assert.False(TodoList.Empty.HasDescription);
            Assert.Equal(0, TodoList.Empty.Count);
        }

        [Fact]
        public void TodoList_Defaults_Correct()
        {
            TodoList todoList = new TodoList("Test TodoList");

            Assert.Equal("Test TodoList", todoList.Name);
            Assert.False(todoList.HasDescription);
            Assert.Equal(0, todoList.Count);
        }

        [Fact]
        public void AddTask_Success()
        {
            TodoList todoList = new TodoList("Test TodoList");
            Task taskA = new Task("Test Task");

            todoList.AddTask(taskA);
            Result<Task> result = todoList.GetTask(0);

            Assert.True(result.IsSuccess);
            Assert.Equal(taskA, result.GetValue());
        }

        [Fact]
        public void GetTask_Success()
        {
            TodoList todoList = new TodoList("Test TodoList");
            Task task = new Task("Test Task");

            todoList.AddTask(task);

            Assert.True(todoList.GetTask(0).IsSuccess);
        }

        [Fact]
        public void GetTask_Failure()
        {
            TodoList todoList = new TodoList("Test TodoList");

            Assert.True(todoList.GetTask(0).IsFailure);
        }

        [Fact]
        public void DeleteTask_Success()
        {
            TodoList todoList = new TodoList("Test TodoList");
            Task task = new Task("Test Task");

            todoList.AddTask(task);

            Assert.True(todoList.DeleteTask(task));
            Assert.True(todoList.GetTask(0).IsFailure);
        }

        [Fact]
        public void MoveTask_Success()
        {
            TodoList a = new TodoList("Test TodoList A");
            TodoList b = new TodoList("Test TodoList B");
            Task task = new Task("Test Task");

            a.AddTask(task);

            // Assert "a" contains the Task while "b" does not
            Assert.True(a.ContainsTask(task));
            Assert.False(b.ContainsTask(task));

            // Assert MoveTask succeeded
            Assert.True(a.MoveTask(task, b));

            // Assert "b" contains the Task while "a" does not
            Assert.False(a.ContainsTask(task));
            Assert.True(b.ContainsTask(task));

            // Assert the moved Task is the same object
            Assert.Same(b.GetTask(0).GetValue(), task);
        }

        [Fact]
        public void CopyTask_Success()
        {
            TodoList a = new TodoList("Test TodoList A");
            TodoList b = new TodoList("Test TodoList B");
            Task task = new Task("Test Task");

            a.AddTask(task);

            // Assert "a" contains the Task while "b" does not
            Assert.True(a.ContainsTask(task));
            Assert.False(b.ContainsTask(task));

            // Assert CopyTask succeeded
            Assert.True(a.CopyTask(task, b));

            // Assert both "a" and "b" contain the Task
            Assert.True(a.ContainsTask(task));
            Assert.True(b.ContainsTask(task));

            // Assert the copied Task is a different
            Assert.NotSame(b.GetTask(0).GetValue(), task);
        }

        [Fact]
        public void CompleteTask_Success()
        {
            TodoList todoList = new TodoList("Test TodoList");
            Task task = new Task("Test Task");

            todoList.AddTask(task);

            Assert.True(todoList.CompleteTask(task));
            Assert.True(task.IsComplete);
        }
    }
}
