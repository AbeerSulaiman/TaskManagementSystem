namespace TaskManagementSystem.GlobalErrorHandler
{
    public class NotFoundBusinessException: Exception
    {
        public NotFoundBusinessException(string message) : base(message)
        {

        }
    }
}
