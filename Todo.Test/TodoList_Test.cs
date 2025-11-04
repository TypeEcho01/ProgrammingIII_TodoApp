using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Todo.Common.Classes;
using Todo.Common.Interfaces;
using Task = Todo.Common.Classes.Task;

namespace Todo.Test
{
    public class TodoList_Test
    {
        [Fact]
        public void Empty_Defaults_Correct()
        {
            Assert.Equal(TodoList.Empty.Name, string.Empty);
            Assert.Null(TodoList.Empty.Description);
            Assert.Equal(0, TodoList.Empty.Count);
        }

        [Fact]
        public void TodoList_Defaults_Correct()
        {
            TodoList todoList = new TodoList("Test TodoList");

            Assert.Equal("Test TodoList", todoList.Name);
            Assert.Null(todoList.Description);
            Assert.Equal(0, todoList.Count);
        }

        [Fact]
        public void AddTask_Success()
        {
            TodoList todoList = new TodoList("Test TodoList");
            Task taskA = new Task("Test Task");

            ID id = todoList.AddTask(taskA);

            Assert.True(todoList.TryGetTask(id, out Task taskB));
            Assert.Equal(taskA, taskB);
            Assert.Equal(1, todoList.Count);
        }

        [Fact]
        public void AddTask_Failure_Task_IsEmpty()
        {
            TodoList todoList = new TodoList("Test TodoList");

            Assert.Throws<ArgumentException>
            (
                () => todoList.AddTask(Task.Empty)
            );
        }

        [Fact]
        public void AddTask_Failure_Name_IsEmpty()
        {
            TodoList todoList = new TodoList("Test TodoList");
            Task task = new Task(string.Empty);

            Assert.Throws<ArgumentException>
            (
                () => todoList.AddTask(task)
            );
        }

        [Fact]
        public void AddTask_Failure_DueDate_IsLate()
        {
            TodoList todoList = new TodoList("Test TodoList");
            Task task = new Task("Test Task", DateTime.MinValue);

            Assert.Throws<ArgumentException>
            (
                () => todoList.AddTask(task)
            );
        }

        [Fact]
        public void GetTask_Success()
        {
            TodoList todoList = new TodoList("Test TodoList");
            Task task = new Task("Test Task");

            ID id = todoList.AddTask(task);

            Assert.Equal(todoList.GetTask(id), task);
        }

        [Fact]
        public void GetTask_Failure()
        {
            TodoList todoList = new TodoList("Test TodoList");

            Assert.Throws<KeyNotFoundException>
            (
                () => todoList.GetTask(new ID())
            );
        }

        [Fact]
        public void DeleteTask_Success()
        {
            TodoList todoList = new TodoList("Test TodoList");
            Task task = new Task("Test Task");

            ID id = todoList.AddTask(task);

            Assert.True(todoList.TryGetTask(id, out var _));
            Assert.True(todoList.DeleteTask(id));
            Assert.False(todoList.TryGetTask(id, out var _));
        }

        [Fact]
        public void MoveTask_Success()
        {
            TodoList a = new TodoList("Test TodoList A");
            TodoList b = new TodoList("Test TodoList B");
            Task task = new Task("Test Task");

            ID id = a.AddTask(task);

            Assert.True(a.TryGetTask(id, out var _));
            Assert.False(b.TryGetTask(id, out var _));
            Assert.True(a.MoveTask(id, b));
            Assert.False(a.TryGetTask(id, out var _));
            Assert.True(b.TryGetTask(id, out var _));
        }

        [Fact]
        public void CopyTask_Success()
        {
            TodoList a = new TodoList("Test TodoList A");
            TodoList b = new TodoList("Test TodoList B");
            Task task = new Task("Test Task");

            ID idA = a.AddTask(task);
            ID idB = a.CopyTask(idA, b);

            Assert.False(ReferenceEquals(idB, ID.Empty));
            Assert.False(ReferenceEquals(idA, idB));
            Assert.True(a.TryGetTask(idA, out var _));
            Assert.True(b.TryGetTask(idB, out var _));
            Assert.False(a.TryGetTask(idB, out var _));
            Assert.False(b.TryGetTask(idA, out var _));
        }

        [Fact]
        public void CompleteTask_Success()
        {
            TodoList todoList = new TodoList("Test TodoList");
            Task task = new Task("Test Task");

            ID id = todoList.AddTask(task);

            Assert.True(todoList.CompleteTask(id));
            Assert.True(task.IsComplete);
        }
    }
}
