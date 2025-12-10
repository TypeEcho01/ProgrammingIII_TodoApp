using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.App.Services
{
    public interface IMessageDialogService
    {
        void Message(string title, string message);

        void Error(string message);
    }
}
