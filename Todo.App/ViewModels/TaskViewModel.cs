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

        public Task Task { get; }

        private readonly Action save;

        public TaskViewModel() :
            this(Task.Empty, () => { }) { }

        public TaskViewModel(Task task, Action saveCallBack)
        {
            this.Task = task;
            this.ToggleCompletion = new Command(this.ToggleCompletion_Execute);
            this.EditName = new Command(this.EditName_Execute);
            this.EditDescription = new Command(this.EditDescription_Execute);
            this.EditDueDate = new Command(this.EditDueDate_Execute);

            this.save = saveCallBack;
        }

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
                this.save();
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
                this.save();
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
                this.RaisePropertyChangedEvent(nameof(this.DueDateString));
                this.RaisePropertyChangedEvent(nameof(this.StatusString));
                this.save();
            }
        }

        public string DueDateString
        {
            get
            {
                if (!this.Task.IsDue)
                    return string.Empty;

                DateTime date = this.Task.DueDate.Date;

                return $"Due {date:D} at {date:t}";
            }
        }

        public string StatusString
        {
            get
            {
                if (this.Task.State == TaskState.Complete)
                    return "Completed";

                if (this.Task.State == TaskState.InProgress)
                {
                    if ((this.Task.IsDue) && (this.Task.DueDate.IsLate()))
                        return "In Progress (Late)";

                    return "In Progress";
                }

                return "Unknown Status";
            }
        }

        public TaskState State
        {
            get => this.Task.State;

            set
            {
                if (this.State == value)
                    return;

                this.Task.State = value;
                this.RaisePropertyChangedEvent(nameof(this.State));
                this.RaisePropertyChangedEvent(nameof(this.StatusString));
                this.save();
            }
        }

        public bool IsComplete => this.Task.IsComplete;
        public bool IsInProgress => this.Task.IsInProgress;

        public bool HasDescription => this.Task.HasDescription;
        public bool IsDue => this.Task.IsDue;

        public string ToggleCompletionString
        {
            get
            {
                if (this.IsInProgress)
                    return "Mark As Complete";

                return "Mark As Incomplete";
            }
        }

        public Command ToggleCompletion { get; }

        private void ToggleCompletion_Execute(object? parameter)
        {
            if (this.Task.IsInProgress)
                this.Task.Complete();
            else
                this.Task.State = TaskState.InProgress;

            this.RaisePropertyChangedEvent(nameof(this.State));
            this.RaisePropertyChangedEvent(nameof(this.StatusString));
            this.RaisePropertyChangedEvent(nameof(this.ToggleCompletionString));
            this.save();
        }

        public Command EditName { get; }

        private void EditName_Execute(object? parameter)
        {
            while (true)
            {
                Result<string> input = App.Input("Edit Name", "Enter Task Name:", this.Task.Name);

                if (input.IsFailure)
                    return;

                string value = input.GetValue();

                if (string.IsNullOrWhiteSpace(value))
                {
                    App.Error("Task cannot have an empty name.");
                    continue;
                }

                this.Name = value;
                return;
            }
        }

        public Command EditDescription { get; }

        private void EditDescription_Execute(object? parameter)
        {
            Result<string> input = App.Input("Edit Description", "Enter Task Description:", this.Task.Description);

            if (input.IsFailure)
                return;

            this.Description = input.GetValue();
        }

        public Command EditDueDate { get; }

        private void EditDueDate_Execute(object? parameter)
        {
            DateTime? defaultDateTime = ((this.Task.IsDue) ? (this.DueDate.Date) : (null));
            Result<DateTime> input = App.DateTimeInput("Edit Due Date", defaultDateTime);

            if (input.IsFailure)
                return;

            this.DueDate = new DueDate(input.GetValue());
        }
    }
}
