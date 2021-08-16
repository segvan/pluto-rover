using System;
using System.Collections.Generic;
using System.Linq;
using PlutoRover.Enums;
using PlutoRover.Exceptions;
using PlutoRover.Models;
using PlutoRover.Navigation.MovementStrategies;

namespace PlutoRover.Navigation
{
    internal class Navigator
    {
        public Position Position { get; private set; }

        private PlanetInformation planetInformation;

        private static readonly Dictionary<Direction, IMovementStrategy> MovementStrategies =
            new Dictionary<Direction, IMovementStrategy>
            {
                {Direction.E, new EastPositionMovementStrategy()},
                {Direction.N, new NorthPositionMovementStrategy()},
                {Direction.W, new WestPositionMovementStrategy()},
                {Direction.S, new SouthPositionMovementStrategy()}
            };

        public void SetInitialPosition(Position newPosition)
        {
            if (planetInformation == null)
            {
                throw new InvalidPositionException("Initial position can not be set without planet information.");
            }

            if (newPosition.X > planetInformation.MaxX
                || newPosition.X < planetInformation.MinX
                || newPosition.Y > planetInformation.MaxY
                || newPosition.Y < planetInformation.MinY)
            {
                throw new InvalidPositionException($"Initial position '{newPosition}' is outside the planet bounds.");
            }

            Position = newPosition;
        }

        public void SetPlatenInformation(PlanetInformation newPlanetInformation)
        {
            planetInformation = newPlanetInformation ?? throw new ArgumentNullException(nameof(newPlanetInformation));
        }

        public void MoveExecute(char moveCommand)
        {
            var newPosition = moveCommand switch
            {
                'F' => MovementStrategies[Position.Direction].MoveForward(Position, planetInformation),
                'B' => MovementStrategies[Position.Direction].MoveBack(Position, planetInformation),
                'L' => MovementStrategies[Position.Direction].TurnLeft(Position),
                'R' => MovementStrategies[Position.Direction].TurnRight(Position),
                _ => throw new NotSupportedCommandException(moveCommand)
            };

            MoveTo(newPosition);
        }

        private void MoveTo(Position newPosition)
        {
            var obstacle = DetectObstacle(newPosition);
            if (obstacle != null)
            {
                throw new ObstacleDetectedException(obstacle);
            }

            Position = newPosition;
        }

        private Obstacle DetectObstacle(Position newPosition)
        {
            return planetInformation.Obstacles.FirstOrDefault(x => x.X == newPosition.X && x.Y == newPosition.Y);
        }
    }
}