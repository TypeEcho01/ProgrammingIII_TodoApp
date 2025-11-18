using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Common
{
    public class Result : IResult
    {
        public bool IsSuccess => (this is Success);
        public bool IsFailure => (this is Failure);

        public string GetMessage()
        {
            if (this is Failure failure)
                return failure.Message;

            throw new ResultException("Result is not a Failure: Message does not exist.");
        }

        public static Success Success() =>
            new Success();

        public static Success<T> Success<T>(T value) =>
            new Success<T>(value);

        public static Failure Failure(string message) =>
            new Failure(message);

        public static Failure<T> Failure<T>(string message) =>
            new Failure<T>(message);
    }

    public abstract class Result<T> : IResult<T>
    {
        public bool IsSuccess => (this is Success<T>);
        public bool IsFailure => (this is Failure<T>);

        public string GetMessage()
        {
            if (this is Failure<T> failure)
                return failure.Message;

            throw new ResultException("Result is not a Failure: Message does not exist.");
        }

        public T GetValue()
        {
            if (this is Success<T> success)
                return success.Value;

            throw new ResultException("Result is not a Success: Value does not exist.");
        }
    }

    // void Success
    public sealed class Success : Result, ISuccess { }

    // T Success
    public sealed class Success<T> : Result<T>, ISuccess
    {
        public T Value { get; }

        internal Success(T value)
        {
            this.Value = value;
        }
    }

    // void Failure
    public sealed class Failure : Result, IFailure
    {
        public string Message { get; }

        internal Failure(string message)
        {
            this.Message = message;
        }
    }

    // T Failure
    public sealed class Failure<T> : Result<T>, IFailure
    {
        public string Message { get; }

        internal Failure(string message)
        {
            this.Message = message;
        }
    }
}
