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

        protected void RaisePropertyChangedEvent([CallerMemberName] string? propertyName = null)
        {
            if (this.PropertyChanged == null)
                throw new InvalidOperationException("Cannot RaisePropertyChangedEvent without a property name.");

            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Command : ICommand
    {
        private ICommandOnExecute execute;
        private ICommandOnCanExecute canExecute;

        public delegate void ICommandOnExecute(object? parameter);
        public delegate bool ICommandOnCanExecute(object? parameter);

        public Command(ICommandOnExecute onExecuteMethod, ICommandOnCanExecute onCanExecuteMethod)
        {
            this.execute = onExecuteMethod;
            this.canExecute = onCanExecuteMethod;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter) =>
            this.canExecute.Invoke(parameter);

        public void Execute(object? parameter) =>
            this.execute.Invoke(parameter);
    }
}
