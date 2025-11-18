using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task = Todo.Common.Task;

namespace Todo.Common
{
    public interface ITodoList : IEnumerable<Task>
    {
        void AddTask(Task task);

        bool CompleteTask(Task task);

        bool ContainsTask(Task task);

        bool CopyTask(Task task, ITodoList source);

        bool DeleteTask(Task task);

        List<Task> GetAllTasks();

        Result<Task> GetTask(ID id);

        Result<Task> GetTask(int index);

        Result<Task> GetTask(Index index);

        Result<Task> GetTask(string name);

        Result<Task> GetTaskByID(ID id);

        Result<Task> GetTaskByIndex(int index);

        Result<Task> GetTaskByIndex(Index index);

        Result<Task> GetTaskByName(string name);

        bool MoveTask(Task task, ITodoList source);

        Task[] ToArray();

        List<Task> ToList();
    }
}
