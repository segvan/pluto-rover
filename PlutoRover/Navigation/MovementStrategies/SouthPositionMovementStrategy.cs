using PlutoRover.Enums;
using PlutoRover.Models;

namespace PlutoRover.Navigation.MovementStrategies
{
    internal class SouthPositionMovementStrategy : IMovementStrategy
    {
        public Position TurnRight(Position position)
        {
            position.Direction = Direction.W;
            return position;
        }

        public Position TurnLeft(Position position)
        {
            position.Direction = Direction.E;
            return position;
        }

        public Position MoveForward(Position position, PlanetInformation planetInformation)
        {
            if (position.Y > planetInformation.MinY)
            {
                position.Y -= 1;
            }
            else
            {
                position.Y = planetInformation.MaxY;
            }

            return position;
        }

        public Position MoveBack(Position position, PlanetInformation planetInformation)
        {
            if (position.Y < planetInformation.MaxY)
            {
                position.Y += 1;
            }
            else
            {
                position.Y = planetInformation.MinY;
            }

            return position;
        }
    }
}