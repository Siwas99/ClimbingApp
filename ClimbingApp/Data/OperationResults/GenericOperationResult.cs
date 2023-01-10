namespace ClimbingApp.Data.OperationResults
{
    public class OperationResult<T> : OperationResult
    {
        public OperationResult(OperationStatus status, Exception exception = null) : base(status, exception)
        {
            Result = default;
        }

        public OperationResult(T result) : base(OperationStatus.Success)
        {
            Result = result;
        }

        public readonly T Result;
    }
}
