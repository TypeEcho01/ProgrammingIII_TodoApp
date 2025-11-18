using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Common
{
    public interface IRepository<T> where T : Entity
    {
        void Add(T entity);

        bool Contains(T entity);

        bool Delete(T entity);

        Result<T> Get(ID id);

        List<T> GetAll();

        Result<T> GetByField<TField>(string name, TField value);

        Result<T> GetByID(ID id);

        Result<T> GetByProperty<TProperty>(string name, TProperty value);

        T[] ToArray();

        List<T> ToList();
    }
}
