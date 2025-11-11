using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Common
{
    public interface ITask
    {
        TaskState State { get; }

        bool IsComplete { get; }

        bool IsInProgress { get; }

        void Complete();
    }
}
