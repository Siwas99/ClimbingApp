using System.Security.Authentication;

namespace ClimbingApp.Data.OperationResults
{
    public class OperationResult
    {
        public OperationResult(OperationStatus status, Exception exception = null)
        {
            Status = status;
            Exception = exception;
        }

        public OperationResult(OperationStatus status, string exception)
        {
            if (status == OperationStatus.Success)
                throw new Exception("Cannot create success OperationResult with exception");

            Status = status;

            Exception = status switch
            {
                OperationStatus.Unauthorized => new AuthenticationException(exception),
                OperationStatus.NotFound => new ArgumentNullException(exception),
                OperationStatus.ConversionError => new InvalidCastException(exception),
                _ => new Exception(exception)
            };
        }

        public OperationResult()
        {
            Status = OperationStatus.Success;
        }

        public readonly OperationStatus Status;
        public Exception Exception;
    }
}