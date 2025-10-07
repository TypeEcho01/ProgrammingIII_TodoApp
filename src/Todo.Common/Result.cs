using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Common
{
    public class Result
    {
        private readonly bool ok;
        private readonly string error;

        private Result()
        {
            this.ok = true;
            this.error = string.Empty;
        }

        private Result(string error)
        {
            this.ok = false;
            this.error = error;
        }

        //public static TReturn Evaluate<TReturn>(Func<Result> func, Func<string, TReturn> onError, Func<TReturn> onOk)
        //{
        //    var r = func();

        //    if (r.IsError())
        //        return onError(r.GetError());

        //    return onOk();
        //}

        public static Result Ok() =>
            new Result();

        public static Result Error(string error) =>
            new Result(error);

        public bool IsError() =>
            !this.ok;

        public bool IsOk() =>
            this.ok;

        public string GetError() =>
            this.error;
    }

    public class Result<T> where T : class
    {
        private readonly bool ok;
        private readonly string error;
        private readonly T? value;

        private Result(T value)
        {
            this.ok = true;
            this.error = string.Empty;
            this.value = value;
        }

        private Result(string error)
        {
            this.ok = false;
            this.error = error;
            this.value = null;
        }

        //public static TReturn Evaluate<TReturn>(Func<Result> func, Func<string, TReturn> onError, Func<TReturn> onOk)
        //{
        //    var r = func();

        //    if (r.IsError())
        //        return onError(r.GetError());

        //    return onOk();
        //}

        public static Result<T> Ok(T value) =>
            new Result<T>(value);

        public static Result<T> Error(string error) =>
            new Result<T>(error);

        public bool IsError() =>
            !this.ok;

        public bool IsOk() =>
        this.ok;

        public string GetError() =>
            this.error;

        public T? GetValue() =>
            this.value;
    }
}
