using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common;

namespace Todo.App.Services
{
    public sealed class InputDialogService : IInputDialogService
    {
        public Result<string> Input(string title, string prompt, string defaultText = "")
        {
            var dialog = new InputDialog(title, prompt, defaultText);

            if (dialog.ShowDialog() == true)
                return Result.Success(dialog.Input);

            return Result.Failure<string>("No input entered.");
        }
    }
}
