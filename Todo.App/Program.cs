using Todo.Common.Classes;
using static EchoLib.Functions;
using static EchoLib.KeywordArguments;
using Task = Todo.Common.Classes.Task;

namespace Todo.App
{
    internal class Program
    {
        static void Main()
        {
            var task = new Task("My Task", "Description of the task.", DateTime.UtcNow.AddDays(4));

            Print(task);
        }
    }
}