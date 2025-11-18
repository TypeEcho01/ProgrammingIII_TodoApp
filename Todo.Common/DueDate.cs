using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Common
{
    public class DueDate : Entity, IDueDate
    {
        public static bool IsEmpty(DueDate dueDate) =>
            ReferenceEquals(dueDate, DueDate.Empty);

        public static DueDate Empty = new DueDate(DateTime.MinValue);

        private static DateTime Now =>
            DateTime.UtcNow;

        public DateTime Date { get; private set; }

        public DueDate(DateTime date)
        {
            this.Type = this.GetType();
            this.ID = new ID();

            this.Date = date;
        }

        public DueDate(int year, int month, int day) : 
            this(new DateTime(year, month, day)) { }

        public DueDate(int year, int month, int day, int hour) :
            this(new DateTime(year, month, day, hour, minute: 0, second: 0)) { }

        public DueDate(int year, int month, int day, int hour, int minute) :
            this(new DateTime(year, month, day, hour, minute, second: 0)) { }

        public bool IsEmpty() =>
            DueDate.IsEmpty(this);

        public bool IsOnTime() =>
            Now < Date;

        public bool IsLate() =>
            Now >= Date;

        public void ChangeDate(DateTime newDate) =>
            Date = newDate;

        public void ChangeDate(DueDate newDate) =>
            Date = newDate.Date;

        public override string ToString() =>
            Date.ToString();
    }
}
