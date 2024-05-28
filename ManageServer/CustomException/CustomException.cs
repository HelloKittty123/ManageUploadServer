namespace ManageServer.CustomException
{
    public class CustomException : Exception
    {
        public CustomException() { }

        public CustomException(string message) : base(message) { }

        public CustomException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException() { }

        public InvalidPasswordException(string message) : base(message) { }

    }

    public class NotFoundUserException : Exception
    {
        public NotFoundUserException() { }

        public NotFoundUserException(string message) : base(message) { }

    }
}
