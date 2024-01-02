namespace AdventOfCode2023.Day10
{
    public enum Direction
    {
        East = 1,
        West,
        North,
        South
    }

    public class Navigator
    {
        public (int y, int x) Position { get; set; }
        public char Next { get; set; }
        public int Counter { get; set; }
        public Direction Direction { get; set; }
    }
}
