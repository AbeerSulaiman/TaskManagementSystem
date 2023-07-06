namespace TaskManagementSystem.GlobalErrorHandler
{
    public class GenericBusinessException: Exception
    {
        public GenericBusinessException(string message) : base(message)
        {
        }
    }
}
