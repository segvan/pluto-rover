using System;
using PlutoRover.Exceptions;
using PlutoRover.Models;
using PlutoRover.Navigation;

namespace PlutoRover
{
    public class Rover
    {
        private readonly Navigator navigator;

        public Rover()
        {
            navigator = new Navigator();
        }

        public Rover SetInitialPosition(Position position)
        {
            navigator.SetInitialPosition(position);
            return this;
        }

        public Rover LoadPlanetInformation(PlanetInformation planetInformation)
        {
            navigator.SetPlatenInformation(planetInformation);
            return this;
        }

        public CommandResult ExecuteNavigationCommands(string commands)
        {
            if (string.IsNullOrEmpty(commands))
            {
                return CommandResult.OkResult(navigator.Position);
            }

            try
            {
                foreach (var command in commands)
                {
                    navigator.MoveExecute(command);
                }
            }
            catch (NotSupportedCommandException e)
            {
                return CommandResult.NotSupportedCommandResult(navigator.Position, e.Message);
            }
            catch (ObstacleDetectedException e)
            {
                return CommandResult.ObstacleDetectedCommandResult(navigator.Position, e.Message);
            }
            catch (Exception e)
            {
                return CommandResult.UnknownErrorResult(navigator.Position, e.Message);
            }

            return CommandResult.OkResult(navigator.Position);
        }
    }
}