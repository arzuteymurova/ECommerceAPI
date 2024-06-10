namespace ECommerceAPI.Application.Exceptions
{
    public class NotFoundUserException : Exception
    {
        public NotFoundUserException() : base("Username or email is incorrect!")
        {
        }

        public NotFoundUserException(string? message) : base(message)
        {   
        }

        public NotFoundUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }

    public class AuthenticationErrorException : Exception
    {
        public AuthenticationErrorException() : base("Authentication failed!")
        {
        }

        public AuthenticationErrorException(string? message) : base(message)
        {
        }

        public AuthenticationErrorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }

}
