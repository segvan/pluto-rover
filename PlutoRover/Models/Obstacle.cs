namespace PlutoRover.Models
{
    public class Obstacle
    {
        public int X { get; }

        public int Y { get; }

        public Obstacle(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }
}