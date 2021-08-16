using System;
using PlutoRover.Enums;

namespace PlutoRover.Models
{
    public struct Position
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Direction Direction { get; set; }

        public Position(int x, int y, Direction direction)
        {
            if (!Enum.IsDefined(typeof(Direction), direction))
            {
                throw new ArgumentOutOfRangeException(nameof(direction),
                    "Value is not defined in a Direction enumeration.");
            }

            X = x;
            Y = y;
            Direction = direction;
        }

        public override string ToString()
        {
            return $"{X},{Y},{Direction}";
        }
    }
}