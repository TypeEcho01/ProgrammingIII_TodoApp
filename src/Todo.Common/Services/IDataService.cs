using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common.Models;

namespace Todo.Common.Services
{
    public interface IDataService<T, TKey>
    {
        Task SaveAsync(T? obj);
        Task<T?> GetAsync(TKey? key);
    }
}
