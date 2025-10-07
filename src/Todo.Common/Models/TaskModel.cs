using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common.Requests;

namespace Todo.Common.Models
{
    public class TaskModel
    {
        // Must exist
        public string Key { get; private set; }

        // Must exist
        public string Name { get; private set; }

        // Optional
        public string Description { get; private set; }

        // Must:
        //  * Exist
        //  * Be in the future
        public DateTime DueDate { get; private set; }
        
        private TaskModel()
        {
            this.Key = string.Empty;
            this.Name = string.Empty;
            this.Description = string.Empty;
            this.DueDate = DateTime.MinValue.ToUniversalTime();
        }

        public static Result<TaskModel> CreateTask(CreateTaskRequest request)
        {
            var validationResult = request.IsValid();
            if (validationResult.IsError())
                return Result<TaskModel>.Error(validationResult.GetError());

            return Result<TaskModel>.Ok
            (
                new TaskModel
                {
                    Key = Guid.NewGuid().ToString(), 
                    Name = request.Name, 
                    Description = request.Description, 
                    DueDate = request.DueDate
                }
            );
        }

        public static Result<TaskModel> UpdateTask(UpdateTaskRequest request)
        {
            var validationResult = request.IsValid();
            if (validationResult.IsError())
                return Result<TaskModel>.Error(validationResult.GetError());

            return Result<TaskModel>.Ok
            (
                new TaskModel
                {
                    Key = request.Key, 
                    Name = request.TaskModel.Name, 
                    Description = request.TaskModel.Description, 
                    DueDate = request.TaskModel.DueDate
                }
            );
        }
    }
}
