using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Todo.Common.Models;

namespace Todo.Common.Requests
{
    public class UpdateTaskRequest
    {
        public string Key { get; }
        public TaskModel TaskModel { get; }

        public UpdateTaskRequest(string? key, TaskModel taskModel)
        {
            this.Key = key ?? string.Empty;
            this.TaskModel = taskModel;
        }

        public Result IsValid()
        {
            if (this.TaskModel == null)
                return Result.Error("TaskModel is empty.");

            if (string.IsNullOrWhiteSpace(this.Key))
                return Result.Error("Key is empty.");

            return Result.Ok();
        }
    }
}
