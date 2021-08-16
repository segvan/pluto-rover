using System;
using PlutoRover.Models;

namespace PlutoRover.Exceptions
{
    public class ObstacleDetectedException : Exception
    {
        public ObstacleDetectedException(Obstacle obstacle) : base($"Obstacle detected. Coordinates: '{obstacle}.'")
        {
        }
    }
}