using System;

namespace Api
{
    public class InvalidMessageException: Exception
    {
        public virtual string Code { get; }

        public InvalidMessageException(string message) : base(message)
        {
        }
    }
}