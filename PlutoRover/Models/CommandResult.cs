using PlutoRover.Enums;

namespace PlutoRover.Models
{
    public class CommandResult
    {
        public ResultCode ResultCode { get; }

        public string Position { get; }

        public string Message { get; }

        public CommandResult(ResultCode resultCode, string position, string message)
        {
            ResultCode = resultCode;
            Position = position;
            Message = message;
        }

        public static CommandResult OkResult(Position position, string message = null)
        {
            return new(ResultCode.Ok, position.ToString(), message);
        }

        public static CommandResult NotSupportedCommandResult(Position position, string message)
        {
            return new(ResultCode.BadRequest, position.ToString(), message);
        }

        public static CommandResult ObstacleDetectedCommandResult(Position position, string message)
        {
            return new(ResultCode.Forbidden, position.ToString(), message);
        }

        public static CommandResult UnknownErrorResult(Position position, string message)
        {
            return new(ResultCode.UnknownError, position.ToString(), message);
        }
    }
}