using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Todo.Common;

using Task = Todo.Common.Task;

namespace Todo.App.ViewModels
{
    public class TodoListViewModel : ViewModel
    {
        public TodoListViewModel()
        {
            foreach (Task task in App.Repository.Load())
                this.Tasks.Add(new TaskViewModel(task, this.SaveTasks));

            this.DeleteTask = new Command(DeleteTask_Execute);
            this.AddTask = new Command(AddTask_Execute);
            this.MoveUp = new Command(MoveUp_Execute);
            this.MoveDown = new Command(MoveDown_Execute);
        }

        private void SaveTasks()
        {
            App.Repository.Save(this.Tasks.Select((viewModel) => viewModel.Task));
        }

        public ObservableCollection<TaskViewModel> Tasks { get; } = new();

        public Command DeleteTask { get; }

        private void DeleteTask_Execute(object? parameter)
        {
            if (parameter is TaskViewModel task)
            {
                this.Tasks.Remove(task);
                this.RaisePropertyChangedEvent(nameof(this.Tasks));
                this.SaveTasks();
            }
        }

        public Command AddTask { get; }

        private void AddTask_Execute(object? parameter)
        {
            string name = "New Task";

            Result<string> input = App.Input("New Task", "Enter the new Task Name:", name);

            if (input.IsFailure)
                return;

            string value = input.GetValue();

            if (string.IsNullOrWhiteSpace(value))
                App.Error("Task cannot have an empty name.");
            else
                name = value;

            var task = new Task(name);
            var viewModel = new TaskViewModel(task, this.SaveTasks);

            this.Tasks.Add(viewModel);

            this.RaisePropertyChangedEvent(nameof(this.Tasks));
            this.SaveTasks();
        }

        public Command MoveUp { get; }

        private void MoveUp_Execute(object? parameter)
        {
            if (parameter is TaskViewModel taskVm)
            {
                int index = this.Tasks.IndexOf(taskVm);
                if (index == -1) // ensure IndexOf found the task
                    return;

                if (index > 0)
                    this.Tasks.Move(index, (index - 1));
                else
                    this.Tasks.Move(index, (this.Tasks.Count - 1)); // wrap around

                this.RaisePropertyChangedEvent(nameof(this.Tasks));
                this.SaveTasks();
            }
        }

        public Command MoveDown { get; }

        private void MoveDown_Execute(object? parameter)
        {
            if (parameter is TaskViewModel taskVm)
            {
                int index = this.Tasks.IndexOf(taskVm);
                if (index == -1) // ensure IndexOf found the task
                    return;

                if (index < (this.Tasks.Count - 1))
                    this.Tasks.Move(index, (index + 1));
                else
                    this.Tasks.Move(index, 0); // wrap around
                
                this.RaisePropertyChangedEvent(nameof(this.Tasks));
                this.SaveTasks();
            }
        }
    }
}
