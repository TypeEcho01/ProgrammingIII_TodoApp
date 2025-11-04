using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Todo.Common.Classes;

namespace Todo.Common.Interfaces
{
    public interface IDue
    {
        DueDate? DueDate { get; }
    }
}
