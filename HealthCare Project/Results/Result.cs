namespace HealthCare_Project.Results
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailue => !IsSuccess;
        public string Error { get; } = string.Empty;

        public Result(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new Result(true, string.Empty);
        public static Result Failure(string error) => new Result(false, error);
    }
}
