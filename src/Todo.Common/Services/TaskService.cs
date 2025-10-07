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

        public async Task<Result> CreateTaskAsync(CreateTaskRequest request)
        {
            var modelResult = TaskModel.CreateTask(request);

            if (modelResult.IsError())
                return Result.Error(modelResult.GetError());

            await this.fileDataService.SaveAsync(modelResult.GetValue());

            return Result.Ok();
        }
    }
}
