using PlutoRover.Enums;
using PlutoRover.Models;

namespace PlutoRover.Navigation.MovementStrategies
{
    internal class EastPositionMovementStrategy : IMovementStrategy
    {
        public Position TurnRight(Position position)
        {
            position.Direction = Direction.S;
            return position;
        }

        public Position TurnLeft(Position position)
        {
            position.Direction = Direction.N;
            return position;
        }

        public Position MoveForward(Position position, PlanetInformation planetInformation)
        {
            if (position.X < planetInformation.MaxX)
            {
                position.X += 1;
            }
            else
            {
                position.X = planetInformation.MinX;
            }

            return position;
        }

        public Position MoveBack(Position position, PlanetInformation planetInformation)
        {
            if (position.X > planetInformation.MinX)
            {
                position.X -= 1;
            }
            else
            {
                position.X = planetInformation.MaxX;
            }

            return position;
        }
    }
}