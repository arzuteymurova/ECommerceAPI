namespace ECommerceAPI.Application.Exceptions
{
    public class UserNameIsNullOrEmptyException : Exception
    {
        public UserNameIsNullOrEmptyException() : base("Username is null or empty!")
        {
        }

        public UserNameIsNullOrEmptyException(string? message) : base(message)
        {
        }

        public UserNameIsNullOrEmptyException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }

}
