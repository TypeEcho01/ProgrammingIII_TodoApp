using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Todo.Common.Interfaces;

namespace Todo.Common.Classes
{
    public class TodoList : ITodoList, IITem
    {
        public static readonly TodoList Empty = new TodoList(string.Empty);

        private Dictionary<ID, Task> Tasks { get; }

        public int Count => 
            Tasks.Count;

        public string Name { get; private set; }
        public string? Description { get; private set; }

        public bool HasDescription =>
            (this.Description is not null);

        public TodoList(string name) : 
            this(name, null) { }

        public TodoList(string name, string? description)
        {
            this.Tasks = new Dictionary<ID, Task>();

            this.Name = name;
            this.Description = description;
        }

        private static void VerifyTask(Task task)
        {
            if (ReferenceEquals(task, Task.Empty))
                throw new ArgumentException("task cannot be Empty.");

            if (string.IsNullOrWhiteSpace(task.Name))
                throw new ArgumentException("task cannot have an empty Name.");

            if ((task.DueDate is not null) && (task.DueDate.IsLate()))
                throw new ArgumentException("task cannot have a DueDate in the past.");
        }

        public ID AddTask(Task task)
        {
            TodoList.VerifyTask(task);

            ID id = new ID();

            this.Tasks.Add
            (
                key: id,
                value: task
            );

            return id;
        }

        public ID AddTask(string name, string? description, DueDate? dueDate) =>
            this.AddTask(new Task(name, description, dueDate));

        public ID AddTask(string name, string? description, DateTime dueDate) =>
            this.AddTask(name, description, new DueDate(dueDate));

        public ID AddTask(string name, string? description) =>
            this.AddTask(name, description, null);

        public ID AddTask(string name, DueDate? dueDate) =>
            this.AddTask(name, null, dueDate);

        public ID AddTask(string name, DateTime dueDate) =>
            this.AddTask(name, null, new DueDate(dueDate));

        public ID AddTask(string name) =>
            this.AddTask(name, null, null);

        public Task GetTask(ID id)
        {
            if (!Tasks.TryGetValue(id, out var task))
                throw new KeyNotFoundException($"Task with ID \"{id}\" was not found.");

            return task;
        }

        public bool TryGetTask(ID id, out Task task)
        {
            try
            {
                task = this.GetTask(id);
                return true;                
            }
            catch (KeyNotFoundException)
            {
                task = Task.Empty;
                return false;
            }
        }

        public bool DeleteTask(ID id) =>
            this.Tasks.Remove(id);

        public bool MoveTask(ID id, TodoList? source)
        {
            if (source is null)
                return false;

            if (!this.TryGetTask(id, out var task))
                return false;

            this.DeleteTask(id);

            source.Tasks.Add(id, task);

            return true;
        }

        public ID CopyTask(ID id, TodoList? source)
        {
            if (source is null)
                return ID.Empty;

            if (!this.TryGetTask(id, out var task))
                return ID.Empty;

            return source.AddTask(task.Copy());
        }

        public bool CompleteTask(ID id)
        {
            if (!this.TryGetTask(id, out var task))
                return false;

            task.Complete();

            return true;
        }

        public IEnumerator<ID> GetIDs()
        {
            foreach (ID id in this.Tasks.Keys)
                yield return id;
        }

        public IEnumerator<Task> GetTasks()
        {
            foreach (Task task in this.Tasks.Values)
                yield return task;
        }

        public IEnumerator<(ID, Task)> GetIDTaskPairs()
        {
            foreach (var pair in this.Tasks)
                yield return (pair.Key, pair.Value);
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            this.GetIDTaskPairs();

        IEnumerator<(ID, Task)> IEnumerable<(ID, Task)>.GetEnumerator() =>
            this.GetIDTaskPairs();
    }
}
