namespace TaskManagementSystem.GlobalErrorHandler
{
        public class ConflictBusinessException : Exception
        {
            public ConflictBusinessException(string message) : base(message)
            {
            }
        }
    }

