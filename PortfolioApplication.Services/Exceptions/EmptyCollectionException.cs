using System;

namespace PortfolioApplication.Services.Exceptions
{
    public class EmptyCollectionException : Exception
    {
        public EmptyCollectionException()
        {

        }

        public EmptyCollectionException(string message) : base(message)
        {

        }

        public EmptyCollectionException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
