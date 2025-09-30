using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task CreateTaskAsync(CreateTaskRequest request)
        {
            // DO THE STUFF
            await Task.CompletedTask;
        }
    }
}
