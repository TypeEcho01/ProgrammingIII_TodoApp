using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common.Models;
using Todo.Common.Requests;
using Todo.Common.Services;
using Todo.Common;

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
        public async Task CreateTask_Success()
        {
            var taskService = new TaskService(this.service);

            var createTaskRequest = new CreateTaskRequest("Test Task", "Dummy description.", DateTime.UtcNow.AddDays(3));
            var createTaskResult = await taskService.CreateTaskAsync(createTaskRequest);

            Assert.True(createTaskResult.IsOk());
        }

        [Fact]
        public async Task CreateTask_Failure_Name()
        {
            var taskService = new TaskService(this.service);

            var createTaskRequest = new CreateTaskRequest("", "Dummy description.", DateTime.UtcNow.AddDays(3));
            var createTaskResult = await taskService.CreateTaskAsync(createTaskRequest);

            Assert.True(createTaskResult.IsError());
        }

        [Fact]
        public async Task CreateTask_Failure_DueDate()
        {
            var taskService = new TaskService(this.service);

            var createTaskRequest = new CreateTaskRequest("Test Task", "Dummy description.", DateTime.MinValue.ToUniversalTime());
            var createTaskResult = await taskService.CreateTaskAsync(createTaskRequest);

            Assert.True(createTaskResult.IsError());
        }

        [Fact]
        public async Task UpdateTask_Success()
        {
            var taskService = new TaskService(this.service);

            var createTask1Request = new CreateTaskRequest("Test Task 1", "Dummy description 1.", DateTime.UtcNow.AddDays(3));
            var createTask1Result = await taskService.CreateTaskAsync(createTask1Request);
            var key1 = createTask1Result.GetValue();
            if (key1 is null)
                Assert.Fail();

            var createTask2Request = new CreateTaskRequest("Test Task 2", "Dummy description 2.", DateTime.UtcNow.AddDays(6));
            var createTask2Result = await taskService.CreateTaskAsync(createTask2Request);
            var key2 = createTask2Result.GetValue();
            if (key2 is null)
                Assert.Fail();

            var task2 = await service.GetAsync(key2);
            if (task2 is null)
                Assert.Fail();

            var updateTaskRequest = new UpdateTaskRequest(key1, task2);
            var updateTaskResult = await taskService.UpdateTaskAsync(updateTaskRequest);

            Assert.True(updateTaskResult.IsOk());
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
