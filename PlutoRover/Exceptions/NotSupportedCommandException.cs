using System;

namespace PlutoRover.Exceptions
{
    public class NotSupportedCommandException : Exception
    {
        public NotSupportedCommandException(char command) : base($"Command '{command}' is not supported.")
        {
        }
    }
}