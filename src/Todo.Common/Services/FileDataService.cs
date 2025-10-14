using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

using Todo.Common.Models;
using Todo.Common.Extensions;

namespace Todo.Common.Services
{
    public interface IFileDataService : IDataService<TaskModel?, string>;

    public class FileDataService : IFileDataService
    {
        private readonly string path;

        // TODO: configure ILogger
        public FileDataService(string path)
        {
            this.path = path;
        }

        public async Task<TaskModel?> GetAsync(string? key)
        {
            if (key is null)
                return null;

            try
            {
                string fileName = TaskModelExtensions.ToFileName(key);
                string combinedPath = Path.Combine(this.path, fileName);
                if (!File.Exists(combinedPath))
                {
                    Console.WriteLine($"FileDataService.GetAsync: File does not exist at path \"{combinedPath}\".");
                    return null;
                }

                using StreamReader sr = new StreamReader(combinedPath);
                string text = await sr.ReadToEndAsync();
                if (string.IsNullOrWhiteSpace(text))
                {
                    Console.WriteLine($"FileDataService.GetAsync: Empty file at path \"{combinedPath}\".");
                    return null;
                }

                return JsonSerializer.Deserialize<TaskModel>(text);

            }
            catch (IOException)
            {
                Console.WriteLine($"FileDataService.GetAsync: Failed to get file for Task \"{key}\".");
                throw;
            }
            catch (JsonException)
            {
                Console.WriteLine($"FileDataService.GetAsync: Deserializing file failed.");
                throw;
            }
            catch (Exception)
            {
                Console.WriteLine($"FileDataService.GetAsync: An exception occured.");
                throw;
            }
        }

        public async Task SaveAsync(TaskModel? obj)
        {
            if (obj is null)
                return;

            // TODO: Test if overwriting is silent
            try
            {
                string fileName = obj.ToFileName();
                string combinedPath = Path.Combine(this.path, fileName);

                using StreamWriter sw = new StreamWriter(combinedPath);
                string text = JsonSerializer.Serialize(obj);

                await sw.WriteAsync(text);
            }
            catch (IOException)
            {
                Console.WriteLine($"FileDataService.SaveAsync: Failed to write to file for Task \"{obj.Key}\".");
                throw;
            }
            catch (JsonException)
            {
                Console.WriteLine($"FileDataService.SaveAsync: Serializing file failed.");
                throw;
            }
            catch (Exception)
            {
                Console.WriteLine($"FileDataService.SaveAsync: An exception occured.");
                throw;
            }
        }
    }
}