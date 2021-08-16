using PlutoRover.Models;

namespace PlutoRover.Navigation
{
    internal interface IMovementStrategy
    {
        Position TurnRight(Position position);

        Position TurnLeft(Position position);

        Position MoveForward(Position position, PlanetInformation planetInformation);

        Position MoveBack(Position position, PlanetInformation planetInformation);
    }
}