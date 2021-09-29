using System;

namespace FloraYFaunaAPI.Exceptions
{
    public class BaseException : Exception
    {
        public int Code { get; set; }
        public string CodeLabel { get; set; }

        public BaseException() { }

        public BaseException(string message) : base(message)
        {
        }

        public BaseException(string message, int code, string codeLabel)
        {
            Code = code;
            CodeLabel = codeLabel;
        }
    }
}