using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Common
{
    public interface IDueDate
    {
        DateTime Date { get; }

        bool IsOnTime();

        bool IsLate();
    }
}
