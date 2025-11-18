using EchoLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Common
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private List<T> Entities { get; }

        public Repository()
        {
            this.Entities = [];
        }

        public void Add(T entity) =>
            this.Entities.Add(entity);

        public bool Contains(T entity) =>
            this.Entities.Contains(entity);

        public bool Delete(T entity) =>
            this.Entities.Remove(entity);

        public Result<T> Get(ID id) =>
            this.GetByID(id);

        public List<T> GetAll() =>
            this.ToList();

        public Result<T> GetByField<TField>(string name, TField? value)
        {
            if (this.Entities.Count != 0)
            {
                T entity = this.Entities[0];

                if (!entity.HasField(name))
                    return Result.Failure<T>($"Entity of type \"{entity.FormatTypeName()}\" does not contain field \"{name}\".");
            }

            foreach (T entity in this.Entities)
            {
                Result<TField?> result = entity.GetNullableProperty<TField>(name);

                if (result.IsFailure)
                    continue;

                if (object.Equals(result.GetValue(), value))
                    return Result.Success(entity);
            }

            return Result.Failure<T>($"Failed to find Entity with field \"{name}\" containing value {value.Representation()}.");
        }

        public Result<T> GetByID(ID id)
        {
            foreach (T entity in this.Entities)
            {
                if (entity.ID == id)
                    return Result.Success(entity);
            }

            return Result.Failure<T>($"Failed to find Entity with ID \"{id}\".");
        }

        public Result<T> GetByProperty<TProperty>(string name, TProperty? value)
        {
            if (this.Entities.Count != 0)
            {
                T entity = this.Entities[0];

                if (!entity.HasProperty(name))
                    return Result.Failure<T>($"Entity of type \"{entity.FormatTypeName()}\" does not contain property \"{name}\".");
            }

            foreach (T entity in this.Entities)
            {
                Result<TProperty?> result = entity.GetNullableProperty<TProperty>(name);

                if (result.IsFailure)
                    continue;

                if (object.Equals(result.GetValue(), value))
                    return Result.Success(entity);
            }

            return Result.Failure<T>($"Failed to find Entity with property \"{name}\" containing value {value.Representation()}.");
        }

        public T[] ToArray() =>
            this.Entities.ToArray();

        public List<T> ToList() =>
            new List<T>(this.Entities);
    }
}
