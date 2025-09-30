using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Common.Models
{
    public class TaskModel
    {
        public string Key { get; set; }
        
        public TaskModel()
        {
            this.Key = string.Empty;
        }
    }
}
