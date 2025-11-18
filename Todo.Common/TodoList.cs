using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Todo.Common
{
    public class TodoList : Entity, ITodoList, IITem
    {
        public static readonly TodoList Empty = new TodoList(string.Empty);

        public static bool IsEmpty(TodoList todoList) =>
            ReferenceEquals(todoList, Task.Empty);

        private List<Task> Tasks { get; }

        public int Count => 
            Tasks.Count;

        public string Name { get; set; }
        public string Description { get; set; }

        public bool HasName =>
            !string.IsNullOrWhiteSpace(Name);

        public bool HasDescription =>
            !string.IsNullOrWhiteSpace(Description);

        public TodoList(string name) : 
            this(name, null) { }

        public TodoList(string name, string? description)
        {
            Tasks = new List<Task>();

            Name = name;
            Description = (string.IsNullOrWhiteSpace(description)) ? (string.Empty) : (description);
        }

        public void AddTask(Task task) =>
            this.Tasks.Add(task);

        public bool CompleteTask(Task task)
        {
            if (!this.ContainsTask(task))
                return false;

            task.Complete();

            return true;
        }

        public bool ContainsTask(Task task) =>
            this.Tasks.Contains(task);

        public bool CopyTask(Task task, ITodoList source)
        {
            if (!this.ContainsTask(task))
                    return false;

            source.AddTask(task.Copy());

            return true;
        }

        public bool DeleteTask(Task task) =>
            this.Tasks.Remove(task);

        public List<Task> GetAllTasks() =>
            this.ToList();

        public Result<Task> GetTask(ID id) =>
            this.GetTaskByID(id);

        public Result<Task> GetTask(int index) =>
            this.GetTaskByIndex(index);

        public Result<Task> GetTask(Index index) =>
            this.GetTaskByIndex(index);

        public Result<Task> GetTask(string name) =>
            this.GetTaskByName(name);

        public Result<Task> GetTaskByID(ID id)
        {
            foreach (Task task in this)
            {
                if (task.ID == id)
                    return Result.Success(task);
            }

            return Result.Failure<Task>($"Failed to find Task with ID \"{id}\".");
        }

        public Result<Task> GetTaskByIndex(int index)
        {
            try
            {
                return Result.Success(this.Tasks[index]);
            }
            catch (IndexOutOfRangeException)
            {
                return Result.Failure<Task>("Index out of range.");
            }
        }

        public Result<Task> GetTaskByIndex(Index index)
        {
            try
            {
                return Result.Success(this.Tasks[index]);
            }
            catch (IndexOutOfRangeException)
            {
                return Result.Failure<Task>("Index out of range.");
            }
        }

        public Result<Task> GetTaskByName(string name)
        {
            foreach (Task task in this)
            {
                if (task.Name == name)
                    return Result.Success(task);
            }

            return Result.Failure<Task>($"Failed to find Task with Name \"{name}\".");
        }

        public bool MoveTask(Task task, ITodoList source)
        {
            if (!this.DeleteTask(task))
                return false;

            source.AddTask(task);

            return true;
        }

        public Task[] ToArray() =>
            this.Tasks.ToArray();

        public List<Task> ToList() =>
            this.Tasks.ToList();

        public IEnumerator<Task> GetEnumerator()
        {
            foreach (Task task in this.Tasks)
                yield return task;
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            this.GetEnumerator();
    }
}
