namespace Domain.Result
{
    public abstract  class Result
    {
        public Error? Error { get; set; }
        public bool  IsSuccess => Error is null;
        public bool IsFailure => Error is not null;

        public static Result<TValue> Ok<TValue>() => new();
        public static Result<TValue> Ok<TValue>(TValue value) => new(value);
        public static Result<TValue> Fail<TValue>(Error error) => new(error);
        public static Result<TValue> Fail<TValue>(string errorCode, string? message) => new(new Error(errorCode, message));
    }
}
