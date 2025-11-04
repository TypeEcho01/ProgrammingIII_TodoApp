using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common.Enums;

namespace Todo.Common.Interfaces
{
    public interface ITask
    {
        TaskState State { get; }

        bool IsComplete { get; }

        bool IsInProgress { get; }

        void Complete();
    }
}
