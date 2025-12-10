using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common;
using Todo.App.Dialogs;

namespace Todo.App.Services
{
    public sealed class DateTimeDialogService : IDateTimeDialogService
    {
        private static DateTime GetDefaultDateTime(DateTime? defaultDateTime)
        {
            if (defaultDateTime is null)
            {
                var now = DateTime.Now;
                return new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
            }

            return (DateTime)defaultDateTime;
        }

        public Result<DateTime> DateTimeInput(string title, DateTime? defaultDateTime = null)
        {
            var dialog = new DateTimeDialog(title, DateTimeDialogService.GetDefaultDateTime(defaultDateTime));

            if (dialog.ShowDialog() == true)
                return Result.Success(dialog.DateTime);

            return Result.Failure<DateTime>("No DateTime entered.");
        }
    }
}
