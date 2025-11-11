using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Common
{
    public interface IITem
    {
        string Name { get; }
        string Description { get; }

        bool HasDescription { get; }
    }
}
