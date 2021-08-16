using System;

namespace PlutoRover.Exceptions
{
    public class InvalidPositionException : Exception
    {
        public InvalidPositionException(string message) : base(message)
        {
        }
    }
}