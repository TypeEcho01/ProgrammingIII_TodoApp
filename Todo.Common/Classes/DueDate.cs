using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common.Interfaces;

namespace Todo.Common.Classes
{
    public class DueDate : IDueDate
    {
        private static DateTime Now =>
            DateTime.UtcNow;

        public DateTime Date { get; private set; }

        public DueDate(DateTime date)
        {
            this.Date = date;
        }

        public DueDate(int year, int month, int day)
        {
            this.Date = new DateTime(year, month, day);
        }

        public DueDate(int year, int month, int day, int hour)
        {
            this.Date = new DateTime(year, month, day, hour, minute: 0, second: 0);
        }

        public DueDate(int year, int month, int day, int hour, int minute)
        {
            this.Date = new DateTime(year, month, day, hour, minute, second: 0);
        }

        public bool IsOnTime() =>
            (DueDate.Now < this.Date);

        public bool IsLate() =>
            (DueDate.Now >= this.Date);

        public void ChangeDate(DateTime newDate) =>
            this.Date = newDate;

        public void ChangeDate(DueDate newDate) =>
            this.Date = newDate.Date;

        public override string ToString() =>
            this.Date.ToString();
    }
}
