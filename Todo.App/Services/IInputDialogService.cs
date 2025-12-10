using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common;

namespace Todo.App.Services
{
    public interface IInputDialogService
    {
        Result<string> Input(string title, string prompt, string defaultText = "");
    }
}
