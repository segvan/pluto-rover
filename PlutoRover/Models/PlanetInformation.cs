using System;
using System.Collections.Generic;

namespace PlutoRover.Models
{
    public class PlanetInformation
    {
        public int MinX => 0;

        public int MinY => 0;

        public int MaxX { get; }

        public int MaxY { get; }

        public IEnumerable<Obstacle> Obstacles { get; }

        public PlanetInformation(int maxX, int maxY)
        {
            if (maxX < MinX)
            {
                throw new ArgumentOutOfRangeException(nameof(maxX), maxX,
                    $"Values range: {MinX} - {int.MaxValue}.");
            }

            if (maxY < MinY)
            {
                throw new ArgumentOutOfRangeException(nameof(maxY), maxY,
                    $"Values range: {MinY} - {int.MaxValue}.");
            }

            MaxX = maxX;
            MaxY = maxY;
            Obstacles = new List<Obstacle>();
        }

        public PlanetInformation(int maxX, int maxY, List<Obstacle> obstacles)
            : this(maxX, maxY)
        {
            Obstacles = obstacles;
        }
    }
}