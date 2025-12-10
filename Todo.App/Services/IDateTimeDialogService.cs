using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common;

namespace Todo.App.Services
{
    public interface IDateTimeDialogService
    {
        Result<DateTime> DateTimeInput(string title, DateTime? defaultDateTime = null);
    }
}
