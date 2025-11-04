using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Todo.Common.Interfaces;
using Todo.Common.Enums;

namespace Todo.Common.Classes
{
    public class Task : ITask, IITem, IDue
    {
        public static readonly Task Empty = new Task(string.Empty);

        public string Name { get; set; }
        public string? Description { get; set; }
        public DueDate? DueDate { get; private set; }
        public TaskState State { get; private set; }

        public bool IsComplete => 
            (this.State == TaskState.Complete);
        public bool IsInProgress => 
            (this.State == TaskState.InProgress);

        public bool HasDescription =>
            (this.Description is not null);

        public bool IsDue =>
            (this.DueDate is not null);

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
            this.Name = name;

            if (string.IsNullOrWhiteSpace(description))
                this.Description = null;
            else
                this.Description = description;

            this.DueDate = dueDate;
            this.State = TaskState.InProgress;
        }

        public Task(string name, string? description, DateTime dueDate) :
            this(name, description, new DueDate(dueDate)) { }

        public override string ToString()
        {
            var builder = new StringBuilder(this.Name);

            if (this.Description is not null)
                builder.Append($": {this.Description}");

            if (this.DueDate is not null)
                builder.Append($" (Due {this.DueDate})");

            return builder.ToString();
        }

        public void Complete()
        {
            this.State = TaskState.Complete;
        }

        public Task Copy() =>
            new Task(this.Name, this.Description, this.DueDate);
    }
}
