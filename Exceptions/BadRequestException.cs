namespace FloraYFaunaAPI.Exceptions
{
    public class BadRequestException: BaseException
    {
        public BadRequestException() { }

        public BadRequestException(string message) : base(message)
        {
        }
    }
}