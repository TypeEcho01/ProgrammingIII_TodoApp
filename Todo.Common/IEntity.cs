using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Common
{
    public interface IEntity
    {
        ID ID { get; }

        Result<T> GetField<T>(string name);

        Result<T?> GetNullableField<T>(string name);

        Result<T> GetProperty<T>(string name);

        Result<T?> GetNullableProperty<T>(string name);

        bool HasField(string name);

        bool HasProperty(string name);
    }
}
