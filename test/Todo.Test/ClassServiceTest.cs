using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common.Models;
using Todo.Common.Services;

namespace Todo.Test
{
    public class ClassServiceTest
    {
        private readonly IFileDataService service;

        public ClassServiceTest() 
        {
            this.service = new DummyFileDataService();
        }

        [Fact]
        public void CreateTaskSucceeds()
        {
            TaskService taskService = new TaskService(this.service);

            // make sure this test passes
        }
    }

    internal class DummyFileDataService : IFileDataService
    {
        private readonly Dictionary<string, TaskModel> data = [];

        public void Seed(TaskModel taskModel) =>
            this.data.Add(taskModel.Key, taskModel);

        public void Seed(IEnumerable<TaskModel> taskModels)
        {
            foreach (TaskModel t in taskModels)
                this.data.Add(t.Key, t);
        }

        public async Task<TaskModel?> GetAsync(string key)
        {
            await Task.CompletedTask;
            
            if (this.data.TryGetValue(key, out TaskModel? value))
                return value;

            return null;
        }

        public async Task SaveAsync(TaskModel? obj)
        {
            await Task.CompletedTask;
            
            if (obj is null)
                return;

            if (this.data.ContainsKey(obj.Key))
                this.data.Remove(obj.Key);

            this.data.Add(obj.Key, obj);
        }
    }
}
