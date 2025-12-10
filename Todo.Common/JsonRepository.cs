using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Todo.Common
{
    public class JsonRepository<T> : Repository<T> where T : Entity
    {
        private string FilePath { get; }

        public JsonRepository(string FilePath)
        {
            this.FilePath = FilePath;
            this.Load();
        }

        public IEnumerable<T> Load()
        {
            this.Clear();

            if (!File.Exists(this.FilePath))
                return new List<T>();

            string json = File.ReadAllText(this.FilePath);

            List<T> result;
            try
            {
                result = JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            }
            catch (JsonException)
            {
                result = new List<T>();
            }

            this.Override(result);

            return result;
        }

        public void Save(IEnumerable<T> entities)
        {
            string json = JsonSerializer.Serialize(entities, new JsonSerializerOptions { WriteIndented = true, IncludeFields = true });
            File.WriteAllText(this.FilePath, json);

            this.Override(entities);
        }
    }
}
