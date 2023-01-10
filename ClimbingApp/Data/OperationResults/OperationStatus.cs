namespace ClimbingApp.Data.OperationResults
{
    public enum OperationStatus
    {
        Success,
        SqlError,
        Exception,
        Unauthorized,
        InvalidOperation,
        ValidationError,
        ConversionError,
        NotFound,
    }
}
