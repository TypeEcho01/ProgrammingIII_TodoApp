using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Todo.Common
{
    public class Task : Entity, ITask, IITem, IDue
    {
        public static readonly Task Empty = new Task(string.Empty);

        public static bool IsEmpty(Task task) =>
            ReferenceEquals(task, Task.Empty);

        public string Name { get; set; }
        public string Description { get; set; }
        public DueDate DueDate { get; set; }
        public TaskState State { get; private set; }

        public bool IsComplete => 
            State == TaskState.Complete;
        public bool IsInProgress => 
            State == TaskState.InProgress;

        public bool HasName =>
            !string.IsNullOrWhiteSpace(Name);

        public bool HasDescription =>
            !string.IsNullOrWhiteSpace(Description);

        public bool IsDue =>
            !ReferenceEquals(DueDate, DueDate.Empty);

        public Task(string name) : 
            this(name, null, null) { }

        public Task(string name, string? description) : 
            this(name, description, null) { }

        public Task(string name, DueDate? dueDate) :
            this(name, null, dueDate) { }

        public Task(string name, DateTime dueDate) :
            this(name, null, new DueDate(dueDate)) { }

        public Task(string name, string? description, DueDate? dueDate)
        {
            this.Type = this.GetType();
            this.ID = new ID();

            this.Name = name;

            if (string.IsNullOrWhiteSpace(description))
                this.Description = string.Empty;
            else
                this.Description = description;

            if (dueDate is null)
                this.DueDate = DueDate.Empty;
            else
                this.DueDate = dueDate;

            this.State = TaskState.InProgress;
        }

        public Task(string name, string? description, DateTime dueDate) :
            this(name, description, new DueDate(dueDate)) { }

        public override string ToString()
        {
            var builder = new StringBuilder(Name);

            if (HasDescription)
                builder.Append($": {Description}");

            if (IsDue)
                builder.Append($" (Due {DueDate})");

            return builder.ToString();
        }

        public void Complete()
        {
            State = TaskState.Complete;
        }

        public Task Copy() =>
            new Task(Name, Description, DueDate);

        public bool IsEmpty() =>
            Task.IsEmpty(this);
    }
}
