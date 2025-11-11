using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Todo.App.ViewModels
{
    public abstract class ViewModel : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged = (sender, e) => { };

        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public abstract class Command : ICommand
    {
        private Action CommandAction { get; }
        private bool CanExecuteCommand { get; }

        public event EventHandler? CanExecuteChanged = (sender, e) => { };

        public Command(Action action)
        {
            this.CommandAction = action;
            this.CanExecuteCommand = true;
        }

        public bool CanExecute(object? parameter) =>
            this.CanExecuteCommand;

        public void Execute(object? parameter) =>
            this.CommandAction();
    }
}
