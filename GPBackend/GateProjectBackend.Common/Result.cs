using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GateProjectBackend.Common
{
    public enum ErrorType
    {
        NoError,
        System,
        NotFound,
        AccessDenied,
        BadRequest
    }
    public class Result<T>
    {
        public T Value { get; }

        public string ErrorMessage { get; set; }

        [JsonIgnore]
        public ErrorType ErrorType { get; set; }

        [JsonIgnore]
        public bool HasValue => Value != null;

        public Result(T value, string errorMessage, ErrorType errorType = ErrorType.NoError)
        {
            ErrorMessage = errorMessage;
            ErrorType = errorType;
            Value = value;
        }

        public static Result<T> Ok(T value)
        {
            return new Result<T>(value, null);
        }

        public static Result<T> Failure(string message)
        {
            return new Result<T>(default, message, ErrorType.System);
        }

        public static Result<T> AccessDenied(string message)
        {
            return new Result<T>(default, message, ErrorType.AccessDenied);
        }

        public static Result<T> NotFound(string message)
        {
            return new Result<T>(default, message, ErrorType.NotFound);
        }

        public static Result<T> BadRequest(string message)
        {
            return new Result<T>(default, message, ErrorType.BadRequest);
        }
    }
}
