using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Todo.Common
{
    public class TodoList : ITodoList, IITem
    {
        public static readonly TodoList Empty = new TodoList(string.Empty);

        private Dictionary<ID, Task> Tasks { get; }

        public int Count => 
            Tasks.Count;

        public string Name { get; private set; }
        public string Description { get; private set; }

        public bool HasDescription =>
            !string.IsNullOrEmpty(Description);

        public TodoList(string name) : 
            this(name, null) { }

        public TodoList(string name, string? description)
        {
            Tasks = new Dictionary<ID, Task>();

            Name = name;
            Description = description ?? string.Empty;
        }

        private static void VerifyTask(Task task)
        {
            if (ReferenceEquals(task, Task.Empty))
                throw new ArgumentException("task cannot be Empty.");

            if (string.IsNullOrWhiteSpace(task.Name))
                throw new ArgumentException("task cannot have an empty Name.");

            if (task.IsDue && task.DueDate.IsLate())
                throw new ArgumentException("task cannot have a DueDate in the past.");
        }

        public ID AddTask(Task task)
        {
            VerifyTask(task);

            ID id = new ID();

            Tasks.Add
            (
                key: id,
                value: task
            );

            return id;
        }

        public ID AddTask(string name, string? description, DueDate? dueDate) =>
            AddTask(new Task(name, description, dueDate));

        public ID AddTask(string name, string? description, DateTime dueDate) =>
            AddTask(name, description, new DueDate(dueDate));

        public ID AddTask(string name, string? description) =>
            AddTask(name, description, null);

        public ID AddTask(string name, DueDate? dueDate) =>
            AddTask(name, null, dueDate);

        public ID AddTask(string name, DateTime dueDate) =>
            AddTask(name, null, new DueDate(dueDate));

        public ID AddTask(string name) =>
            AddTask(name, null, null);

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
                task = GetTask(id);
                return true;                
            }
            catch (KeyNotFoundException)
            {
                task = Task.Empty;
                return false;
            }
        }

        public bool DeleteTask(ID id) =>
            Tasks.Remove(id);

        public bool MoveTask(ID id, TodoList? source)
        {
            if (source is null)
                return false;

            if (!TryGetTask(id, out var task))
                return false;

            DeleteTask(id);

            source.Tasks.Add(id, task);

            return true;
        }

        public ID CopyTask(ID id, TodoList? source)
        {
            if (source is null)
                return ID.Empty;

            if (!TryGetTask(id, out var task))
                return ID.Empty;

            return source.AddTask(task.Copy());
        }

        public bool CompleteTask(ID id)
        {
            if (!TryGetTask(id, out var task))
                return false;

            task.Complete();

            return true;
        }

        public IEnumerator<ID> GetIDs()
        {
            foreach (ID id in Tasks.Keys)
                yield return id;
        }

        public IEnumerator<Task> GetTasks()
        {
            foreach (Task task in Tasks.Values)
                yield return task;
        }

        public IEnumerator<(ID, Task)> GetIDTaskPairs()
        {
            foreach (var pair in Tasks)
                yield return (pair.Key, pair.Value);
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetIDTaskPairs();

        IEnumerator<(ID, Task)> IEnumerable<(ID, Task)>.GetEnumerator() =>
            GetIDTaskPairs();
    }
}
