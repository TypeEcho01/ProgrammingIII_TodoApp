using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common.Requests;

namespace Todo.Common.Services
{
    public interface ITaskService
    {
        Task<Result<string>> CreateTaskAsync(CreateTaskRequest request);

        Task<Result> UpdateTaskAsync(UpdateTaskRequest request);
    }
}