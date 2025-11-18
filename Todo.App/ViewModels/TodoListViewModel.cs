using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Todo.Common;

using Task = Todo.Common.Task;

namespace Todo.App.ViewModels
{
    public class TodoListViewModel : ViewModel
    {
        public static TodoListViewModel Empty = new TodoListViewModel(TodoList.Empty);

        private TodoList TodoList { get; }

        public TodoListViewModel(TodoList todoList)
        {
            this.TodoList = todoList;
        }

        public ID ID => this.TodoList.ID;

        public int Count => this.TodoList.Count;

        public string Name
        {
            get => this.TodoList.Name;

            set
            {
                if (this.Name == value)
                    return;

                this.TodoList.Name = value;
                this.RaisePropertyChangedEvent(nameof(this.Name));
            }
        }

        public string Description
        {
            get => this.TodoList.Description;

            set
            {
                if (this.Description == value)
                    return;

                this.TodoList.Description = value;
                this.RaisePropertyChangedEvent(nameof(this.Description));
            }
        }

        public bool HasName => this.TodoList.HasName;

        public bool HasDescription => this.TodoList.HasDescription;
    }
}
