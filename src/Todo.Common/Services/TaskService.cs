using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common.Models;
using Todo.Common.Requests;

namespace Todo.Common.Services
{
    public class TaskService : ITaskService
    {
        private readonly IFileDataService fileDataService;

        public TaskService(IFileDataService fileDataService)
        {
            this.fileDataService = fileDataService;
        }

        public async Task<Result<string>> CreateTaskAsync(CreateTaskRequest request)
        {
            Result<TaskModel> modelResult = TaskModel.CreateTask(request);
            if (modelResult.IsError())
                return Result<string>.Error(modelResult.GetError());

            TaskModel? taskModel = modelResult.GetValue();
            if (taskModel is null)
                return Result<string>.Error("TaskModel is null.");

            await this.fileDataService.SaveAsync(taskModel);

            return Result<string>.Ok(taskModel.Key);
        }

        public async Task<Result> UpdateTaskAsync(UpdateTaskRequest request)
        {
            var modelResult = TaskModel.UpdateTask(request);
            if (!modelResult.IsError())
                return Result.Error(modelResult.GetError());

            await this.fileDataService.SaveAsync(modelResult.GetValue());

            return Result.Ok();
        }
    }
}
