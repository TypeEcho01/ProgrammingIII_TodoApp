using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Todo.Common;

using Task = Todo.Common.Task;

namespace Todo.App.ViewModels
{
    public sealed class TaskViewModel : ViewModel
    {
        public static TaskViewModel Empty = new TaskViewModel();

        private Task Task { get; }

        public TaskViewModel() :
            this(Task.Empty) { }

        public TaskViewModel(Task task)
        {
            this.Task = task;
            this.Complete = new Command(this.Complete_Execute, this.Complete_CanExecute);
        }

        public TaskViewModel(string name) :
            this(new Task(name)) { }

        public TaskViewModel(string name, string? description) :
            this(new Task(name, description)) { }

        public TaskViewModel(string name, DueDate? dueDate) :
            this(new Task(name, dueDate)) { }

        public TaskViewModel(string name, DateTime dueDate) :
            this(new Task(name, dueDate)) { }

        public TaskViewModel(string name, string? description, DueDate? dueDate) :
            this(new Task(name, description, dueDate)) { }

        public TaskViewModel(string name, string? description, DateTime dueDate) :
            this(new Task(name, description, dueDate)) { }

        public ID ID => this.Task.ID;

        public string Name
        {
            get => this.Task.Name;

            set
            {
                if (this.Name == value)
                    return;

                this.Task.Name = value;
                this.RaisePropertyChangedEvent(nameof(this.Name));
            }
        }

        public string Description
        {
            get => this.Task.Description;

            set
            {
                if (this.Description == value)
                    return;

                this.Task.Description = value;
                this.RaisePropertyChangedEvent(nameof(this.Description));
            }
        }

        public DueDate DueDate
        {
            get => this.Task.DueDate;

            set
            {
                if (this.DueDate == value)
                    return;

                this.Task.DueDate = value;
                this.RaisePropertyChangedEvent(nameof(this.DueDate));
            }
        }
        public TaskState State => this.Task.State;

        public bool IsComplete => this.Task.IsComplete;
        public bool IsInProgress => this.Task.IsInProgress;

        public bool HasDescription => this.Task.HasDescription;
        public bool IsDue => this.Task.IsDue;

        public Command Complete { get; }

        private bool Complete_CanExecute(object? parameter) =>
            true;

        private void Complete_Execute(object? parameter)
        {
            this.Task.Complete();
            this.RaisePropertyChangedEvent(nameof(this.State));
        }
    }
}
