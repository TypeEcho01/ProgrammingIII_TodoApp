using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Common.Requests
{
    public class CreateTaskRequest
    {
        public string Name { get; }
        public string Description { get; }
        public DateTime DueDate { get; }

        public CreateTaskRequest(string? name, string? description, DateTime dueDate)
        {
            this.Name = name ?? string.Empty;
            this.Description = description ?? string.Empty;
            this.DueDate = dueDate;
        }

        public Result IsValid()
        {
            if (string.IsNullOrWhiteSpace(this.Name))
                return Result.Error("Name required.");

            if (this.DueDate <= DateTime.UtcNow)
                return Result.Error("DueDate in the past.");

            return Result.Ok();
        }
    }
}
