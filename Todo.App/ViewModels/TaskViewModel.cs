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
        private Task Task { get; }

        public string Name => this.Task.Name;
        public string Description => this.Task.Description;
        public DueDate DueDate => this.Task.DueDate;
        public TaskState State => this.Task.State;

        public bool IsComplete => this.Task.IsComplete;
        public bool IsInProgress => this.Task.IsInProgress;

        public bool HasDescription => this.Task.HasDescription;
        public bool IsDue => this.Task.IsDue;

        public TaskViewModel(Task task)
        {
            this.Task = task;
            //this.Complete = new 
        }

        public Command Complete { get; }

        private bool Complete_CanExecute(object? parameter) =>
            this.Complete.CanExecute(parameter);

        private void Complete_Execute(object? parameter)
        {
            this.Complete.Execute(parameter);
            //this.OnPropertyChanged("State");
        }
    }
}
