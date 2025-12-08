namespace Application.Common.Models
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public List<string> Errors { get; }

        protected Result(bool isSuccess, List<string> errors)
        {
            if (isSuccess && !errors.Any())
                throw new InvalidOperationException("Success result cannot have an error");

            if (!isSuccess && errors.Any())
                throw new InvalidOperationException("Failure result must have an error");

            IsSuccess = isSuccess;
            Errors = errors;
        }

        public static Result Success() => new(true, new());
        public static Result Failure(List<string> errors) => new(false, errors);
        public static Result<T> Success<T>(T value) => new(value, true, new());
        public static Result<T> Failure<T>(List<string> errors) => new(default!, false, errors);
    }

    public class Result<T> : Result
    {
        public T Value { get; }

        protected internal Result(T value, bool isSuccess, List<string> error) : base(isSuccess, error)
        {
            Value = value;
        }
    }
}
