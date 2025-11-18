using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Common
{
    public class ResultException : Exception
    {
        public ResultException() :
            base() { }

        public ResultException(string? message) : 
            base(message) { }
    }
}
