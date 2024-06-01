namespace Domain.Result
{
    public class Result<TValue> : Result
    {
        public TValue? Value { get; set; }

        internal Result()
        {
            Value = default;
        }
        internal Result(TValue value)
        {
            Value = value;
        }
        internal Result(Error error)
        {
            Value = default;
            Error = error;
        }

        public static implicit operator Result<TValue>(TValue value) => new(value);
        public static implicit operator Result<TValue>(Error error) => new(error);
    }
}
