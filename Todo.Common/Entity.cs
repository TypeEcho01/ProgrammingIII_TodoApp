using EchoLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Common
{
    public abstract class Entity : IEntity
    {
        protected Type Type { get; }

        public ID ID { get; }

        private string GetTypeName() =>
            this.Type.FormatName();

        protected Entity()
        {
            this.Type = this.GetType();
            this.ID = new ID();
        }

        public Result<T> GetField<T>(string name)
        {
            Result<T?> result = this.GetNullableField<T>(name);

            if (result is Failure failure)
                return Result.Failure<T>(failure.Message);

            T? value = result.GetValue();

            if (value == null)
                return Result.Failure<T>($"{this.GetTypeName()} field \"{name}\" returned null.");

            return Result.Success(value);
        }

        public Result<T?> GetNullableField<T>(string name)
        {
            FieldInfo? field = this.Type.GetField(name);

            if (field is null)
                return Result.Failure<T?>($"{this.GetTypeName()} does not contain field \"{name}\".");

            T? value = (T?)field.GetValue(this);

            return Result.Success(value);
        }

        public Result<T> GetProperty<T>(string name)
        {
            Result<T?> result = this.GetNullableProperty<T>(name);

            if (result is Failure failure)
                return Result.Failure<T>(failure.Message);

            T? value = result.GetValue();

            if (value == null)
                return Result.Failure<T>($"{this.GetTypeName()} property \"{name}\" returned null.");

            return Result.Success(value);
        }

        public Result<T?> GetNullableProperty<T>(string name)
        {
            PropertyInfo? property = this.Type.GetProperty(name);

            if (property is null)
                return Result.Failure<T?>($"{this.GetTypeName()} does not contain property \"{name}\".");

            T? value = (T?)property.GetValue(this);

            return Result.Success(value);
        }

        public bool HasField(string name) =>
            (this.Type.GetField(name) is not null);

        public bool HasProperty(string name) =>
            (this.Type.GetProperty(name) is not null);
    }
}
