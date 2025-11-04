using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Todo.Common.Classes;
using Task = Todo.Common.Classes.Task;

namespace Todo.Common.Interfaces
{
    public interface ITodoList : IEnumerable<(ID, Task)>
    {
        ID AddTask(Task task);

        ID AddTask(string name, string? description, DueDate? dueDate);

        ID AddTask(string name, string? description, DateTime dueDate);

        ID AddTask(string name, string? description);

        ID AddTask(string name, DueDate? dueDate);

        ID AddTask(string name, DateTime dueDate);

        ID AddTask(string name);

        Task GetTask(ID id);

        bool TryGetTask(ID id, out Task task);

        bool DeleteTask(ID id);

        bool MoveTask(ID id, TodoList? source);

        ID CopyTask(ID id, TodoList? source);

        bool CompleteTask(ID id);

        IEnumerator<ID> GetIDs();

        IEnumerator<Task> GetTasks();

        IEnumerator<(ID, Task)> GetIDTaskPairs();
    }
}
