using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Common
{
    public interface IResult
    {
        bool IsSuccess { get; }
        bool IsFailure { get; }

        string GetMessage();
    }

    public interface IResult<T> : IResult
    {
        T GetValue();
    }

    public interface ISuccess : IResult { }

    public interface ISuccess<T> : ISuccess, IResult<T>
    {
        T Value { get; }
    }

    public interface IFailure : IResult
    {
        string Message { get; }
    }

    public interface IFailure<T> : IFailure, IResult<T> { }
}
