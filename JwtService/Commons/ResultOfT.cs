using JwtService.Commons.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtService.Commons
{
    public class Result<T> : IResult
    {
        public static bool operator true(Result<T> result) { return result.Succeeded; }

        public static bool operator false(Result<T> result) { return !result.Succeeded; }

        public static bool operator !(Result<T> result) { return !result.Succeeded; }

        public static implicit operator Result(Result<T> result)
        {
            return result.Cast<bool?>();
        }

        public Result()
        {

        }

        public Result(string errorMessage)
        {
            AddError(errorMessage);
        }

        public T Value { get; private set; }

        public List<string> Errors { get; protected set; } = new List<string>();

        public bool Succeeded => Errors.Count == 0;

        public bool Failed => !Succeeded;

        public Result<T> Ok(T value)
        {
            Value = value;
            Errors = new List<string>();
            return this;
        }

        public void AddError(string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
                throw new ArgumentNullException(nameof(errorMessage));

            Errors.Add(errorMessage);
            Value = default(T);
        }

        public Result<T2> Cast<T2>()
        {
            var newResult = new Result<T2>();
            newResult.Errors = this.Errors;
            return newResult;
        }
    }
}
