using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Todo.Common.Models
{
    public class Task : ITask, IITem, IDue
    {
        public static readonly Task Empty = new Task(string.Empty);

        public string Name { get; set; }
        public string Description { get; set; }
        public DueDate DueDate { get; private set; }
        public TaskState State { get; private set; }

        public bool IsComplete => 
            State == TaskState.Complete;
        public bool IsInProgress => 
            State == TaskState.InProgress;

        public bool HasDescription =>
            !string.IsNullOrEmpty(Description);

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
            Name = name;

            if (string.IsNullOrWhiteSpace(description))
                Description = string.Empty;
            else
                Description = description;

            if (dueDate is null)
                DueDate = DueDate.Empty;
            else
                DueDate = dueDate;

            State = TaskState.InProgress;
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
    }
}
