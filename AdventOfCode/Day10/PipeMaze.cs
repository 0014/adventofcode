using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day10
{
    public static class PipeMaze
    {
        public static int LoopThroughMaze()
        {
            var mazeLines= File.ReadAllLines("Day10\\maze.txt");

            //convert maze into char[][]
            var maze = new char[mazeLines.Length][];
            for (var i = 0; i < mazeLines.Length; i++)
                maze[i] = mazeLines[i].ToCharArray();

            // findout the initial position
            var position = (-1, -1);
            for (var y = 0; y < maze.Length; y++)
            {
                for (var x = 0; x < maze[y].Length; x++)
                {
                    if (maze[y][x] == 'S') position = (y, x);
                }
            }

            // findout the initial direction to navigate
            var navigator = new Navigator { Position = position, Counter = 0};
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                switch (direction)
                {
                    case Direction.North:
                        if ( position.Item1 > 0 && (maze[position.Item1 - 1][position.Item2] == '|' || maze[position.Item1 - 1][position.Item2] == 'F' || maze[position.Item1 - 1][position.Item2] == '7'))
                            navigator.Direction = Direction.North;
                        break;
                    case Direction.South:
                        if (position.Item1 < maze.Length - 1 && (maze[position.Item1 + 1][position.Item2] == '|' || maze[position.Item1 + 1][position.Item2] == 'L' || maze[position.Item1 + 1][position.Item2] == 'J'))
                            navigator.Direction = Direction.South;
                        break;
                    case Direction.East:
                        if (position.Item2 < maze[position.Item1].Length - 1 && (maze[position.Item1][position.Item2 + 1] == '-' || maze[position.Item1][position.Item2 + 1] == 'J' || maze[position.Item1][position.Item2 + 1] == '7'))
                            navigator.Direction = Direction.East;
                        break;
                    case Direction.West:
                        if (position.Item2 > 0 && (maze[position.Item1][position.Item2 - 1] == '-' || maze[position.Item1][position.Item2 - 1] == 'F' || maze[position.Item1][position.Item2 - 1] == 'L'))
                            navigator.Direction = Direction.West;
                        break;
                }
            }

            // Start navigation
            while (navigator.Next != 'S')
            {
                switch (navigator.Direction)
                {
                    case Direction.North:
                        navigator.Position = (navigator.Position.y - 1, navigator.Position.x);
                        navigator.Next = maze[navigator.Position.y][navigator.Position.x];
                        if(navigator.Next == '7') navigator.Direction = Direction.West;
                        else if (navigator.Next == 'F') navigator.Direction = Direction.East;
                        break;
                    case Direction.South:
                        navigator.Position = (navigator.Position.y + 1, navigator.Position.x);
                        navigator.Next = maze[navigator.Position.y][navigator.Position.x];
                        if (navigator.Next == 'J') navigator.Direction = Direction.West;
                        else if (navigator.Next == 'L') navigator.Direction = Direction.East;
                        break;
                    case Direction.East:
                        navigator.Position = (navigator.Position.y, navigator.Position.x + 1);
                        navigator.Next = maze[navigator.Position.y][navigator.Position.x];
                        if (navigator.Next == '7') navigator.Direction = Direction.South;
                        else if (navigator.Next == 'J') navigator.Direction = Direction.North;
                        break;
                    case Direction.West:
                        navigator.Position = (navigator.Position.y, navigator.Position.x - 1);
                        navigator.Next = maze[navigator.Position.y][navigator.Position.x];
                        if (navigator.Next == 'F') navigator.Direction = Direction.South;
                        else if (navigator.Next == 'L') navigator.Direction = Direction.North;
                        break;
                }
                navigator.Counter++;
            }

            return (navigator.Counter + 1) / 2;
        }

        public static int InnerMaze()
        {
            var mazeLines = File.ReadAllLines("Day10\\maze.txt");

            //convert maze into char[][]
            var maze = new char[mazeLines.Length][];
            for (var i = 0; i < mazeLines.Length; i++)
                maze[i] = mazeLines[i].ToCharArray();

            //use a mask to determine the outside and inside of maze
            var maskedMaze = new char[mazeLines.Length][];
            for (var i = 0; i < mazeLines.Length; i++)
                maskedMaze[i] = mazeLines[i].ToCharArray();

            // findout the initial position
            var position = (-1, -1);
            for (var y = 0; y < maze.Length; y++)
            {
                for (var x = 0; x < maze[y].Length; x++)
                {
                    if (maze[y][x] == 'S') position = (y, x);
                }
            }

            // findout the initial direction to navigate
            var navigator = new Navigator { Position = position, Counter = 0 };
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                switch (direction)
                {
                    case Direction.North:
                        if (position.Item1 > 0 && (maze[position.Item1 - 1][position.Item2] == '|' || maze[position.Item1 - 1][position.Item2] == 'F' || maze[position.Item1 - 1][position.Item2] == '7'))
                            navigator.Direction = Direction.North;
                        break;
                    case Direction.South:
                        if (position.Item1 < maze.Length - 1 && (maze[position.Item1 + 1][position.Item2] == '|' || maze[position.Item1 + 1][position.Item2] == 'L' || maze[position.Item1 + 1][position.Item2] == 'J'))
                            navigator.Direction = Direction.South;
                        break;
                    case Direction.East:
                        if (position.Item2 < maze[position.Item1].Length - 1 && (maze[position.Item1][position.Item2 + 1] == '-' || maze[position.Item1][position.Item2 + 1] == 'J' || maze[position.Item1][position.Item2 + 1] == '7'))
                            navigator.Direction = Direction.East;
                        break;
                    case Direction.West:
                        if (position.Item2 > 0 && (maze[position.Item1][position.Item2 - 1] == '-' || maze[position.Item1][position.Item2 - 1] == 'F' || maze[position.Item1][position.Item2 - 1] == 'L'))
                            navigator.Direction = Direction.West;
                        break;
                }
            }

            // Start navigation
            while (navigator.Next != 'S')
            {
                switch (navigator.Direction)
                {
                    case Direction.North:
                        navigator.Position = (navigator.Position.y - 1, navigator.Position.x);
                        navigator.Next = maze[navigator.Position.y][navigator.Position.x];
                        if (navigator.Next == '7') navigator.Direction = Direction.West;
                        else if (navigator.Next == 'F') navigator.Direction = Direction.East;
                        break;
                    case Direction.South:
                        navigator.Position = (navigator.Position.y + 1, navigator.Position.x);
                        navigator.Next = maze[navigator.Position.y][navigator.Position.x];
                        if (navigator.Next == 'J') navigator.Direction = Direction.West;
                        else if (navigator.Next == 'L') navigator.Direction = Direction.East;
                        break;
                    case Direction.East:
                        navigator.Position = (navigator.Position.y, navigator.Position.x + 1);
                        navigator.Next = maze[navigator.Position.y][navigator.Position.x];
                        if (navigator.Next == '7') navigator.Direction = Direction.South;
                        else if (navigator.Next == 'J') navigator.Direction = Direction.North;
                        break;
                    case Direction.West:
                        navigator.Position = (navigator.Position.y, navigator.Position.x - 1);
                        navigator.Next = maze[navigator.Position.y][navigator.Position.x];
                        if (navigator.Next == 'F') navigator.Direction = Direction.South;
                        else if (navigator.Next == 'L') navigator.Direction = Direction.North;
                        break;
                }

                maskedMaze[navigator.Position.y][navigator.Position.x] = 'M';
                navigator.Counter++;
            }

            maskedMaze.ApplyMask(maze);

            maskedMaze.ScanLeft();
            maskedMaze.ScanTop();
            maskedMaze.ScanRight();
            maskedMaze.ScanBottom();

            maskedMaze.PrintMaskedMaze();

            return maskedMaze.SelectMany(line => line).Count(c => c == 'I');
        }

        private static void ApplyMask(this char[][] maskedMaze, char[][] maze)
        {
            for (var i = 0; i < maskedMaze.Length; i ++)
            {
                for (var j = 0; j < maskedMaze[i].Length; j++)
                {
                    if (maskedMaze[i][j] == 'M') maskedMaze[i][j] = maze[i][j];
                    else maskedMaze[i][j] = 'I';
                }
            }
        }

        private static void ScanLeft(this char[][] mask)
        {
            for (var i = 0; i < mask.Length; i ++)
            {
                if (mask[i][0] != 'I') continue;
                mask.FloodMaze((i, 0));
            }
        }

        private static void ScanTop(this char[][] mask)
        {
            for (var i = 0; i < mask[0].Length; i++)
            {
                if (mask[0][i] != 'I') continue;
                mask.FloodMaze((0, i));
            }
        }

        private static void ScanRight(this char[][] mask)
        {
            for (var i = 0; i < mask.Length; i++)
            {
                if (mask[i][mask[i].Length - 1] != 'I') continue;
                mask.FloodMaze((i, mask[i].Length - 1));
            }
        }

        private static void ScanBottom(this char[][] mask)
        {
            for (var i = 0; i < mask[0].Length; i++)
            {
                if (mask[mask.Length - 1][i] != 'I') continue;
                mask.FloodMaze((mask.Length - 1, i));
            }
        }

        private static void FloodMaze(this char[][] mask, (int y, int x) origin)
        {
            if (origin.y < 0 || origin.x < 0 || origin.y == mask.Length || origin.x == mask[origin.y].Length) return;

            if (mask[origin.y][origin.x] == 'S' ||
                mask[origin.y][origin.x] == 'L' ||
                mask[origin.y][origin.x] == '7' || 
                mask[origin.y][origin.x] == 'F' || 
                mask[origin.y][origin.x] == 'J' || 
                mask[origin.y][origin.x] == '-' || 
                mask[origin.y][origin.x] == '|' || 
                mask[origin.y][origin.x] == 'O') return;

            mask[origin.y][origin.x] = 'O';
            
            mask.FloodMaze((origin.y - 1, origin.x));
            mask.FloodMaze((origin.y + 1, origin.x));
            mask.FloodMaze((origin.y, origin.x - 1));
            mask.FloodMaze((origin.y, origin.x + 1));

            mask.FloodSqueezed(origin);
        }

        private static void FloodSqueezed(this char[][] mask, (int y, int x) origin)
        {
            var direction = 0;
            // check if can squeeze up
            if (origin.y - 1 >= 0)
            {
                for (var i = origin.y - 1; i >= 0; i--)
                {
                    if (mask[i][origin.x] == 'I') mask.FloodMaze((i, origin.x));
                    if (origin.x - 1 >= 0 && (direction == 0 || direction == 1) && (mask[i][origin.x] == 'L' || mask[i][origin.x] == 'F' || mask[i][origin.x] == '|'))
                    {
                        if (mask[i][origin.x - 1] == 'J' || mask[i][origin.x - 1] == '7' || mask[i][origin.x - 1] == '|')
                        {
                            direction = 1;
                            continue;
                        }
                        if (mask[i][origin.x - 1] == 'I') mask.FloodMaze((i, origin.x - 1));
                        break;
                    }
                    if (origin.x + 1 < mask[i].Length && (direction == 0 || direction == 2) && (mask[i][origin.x] == 'J' || mask[i][origin.x] == '7' || mask[i][origin.x] == '|'))
                    {
                        if (mask[i][origin.x + 1] == 'L' || mask[i][origin.x + 1] == 'F' || mask[i][origin.x + 1] == '|')
                        {
                            direction = 2;
                            continue;
                        }
                        if (mask[i][origin.x + 1] == 'I') mask.FloodMaze((i, origin.x + 1));
                    }
                    break;
                }
            }

            direction = 0;
            if (origin.y + 1 < mask.Length)
            {
                // check if can squeeze down
                for (var i = origin.y + 1; i < mask.Length; i++)
                {
                    if (mask[i][origin.x] == 'I') mask.FloodMaze((i, origin.x));
                    if (origin.x - 1 >= 0 && (direction == 0 || direction == 1) && (mask[i][origin.x] == 'L' || mask[i][origin.x] == 'F' || mask[i][origin.x] == '|'))
                    {
                        if (mask[i][origin.x - 1] == 'J' || mask[i][origin.x - 1] == '7' || mask[i][origin.x - 1] == '|')
                        {
                            direction = 1;
                            continue;
                        }
                        if (mask[i][origin.x - 1] == 'I') mask.FloodMaze((i, origin.x - 1));
                        break;
                    }
                    if (origin.x + 1 < mask[i].Length && (direction == 0 || direction == 2) && (mask[i][origin.x] == 'J' || mask[i][origin.x] == '7' || mask[i][origin.x] == '|'))
                    {
                        if (mask[i][origin.x + 1] == 'L' || mask[i][origin.x + 1] == 'F' || mask[i][origin.x + 1] == '|')
                        {
                            direction = 2;
                            continue;
                        }
                        if (mask[i][origin.x + 1] == 'I') mask.FloodMaze((i, origin.x + 1));
                    }
                    break;
                }
            }

            direction = 0;
            if (origin.x - 1 >= 0)
            {
                // check if can squeeze left
                for (var i = origin.x - 1; i >= 0; i--)
                {
                    if (mask[origin.y][i] == 'I') mask.FloodMaze((origin.y, i));
                    if (origin.y - 1 >= 0 && (direction == 0 || direction == 1) && (mask[origin.y][i] == '7' || mask[origin.y][i] == 'F' || mask[origin.y][i] == '-'))
                    {
                        if (mask[origin.y - 1][i] == 'L' || mask[origin.y - 1][i] == 'J' || mask[origin.y - 1][i] == '-')
                        {
                            direction = 1;
                            continue;
                        }
                        if (mask[origin.y - 1][i] == 'I') mask.FloodMaze((origin.y - 1, i));
                        break;
                    }
                    if (origin.y + 1 < mask.Length && (direction == 0 || direction == 2) && (mask[origin.y][i] == 'L' || mask[origin.y][i] == 'J' || mask[origin.y][i] == '-'))
                    {
                        if (mask[origin.y + 1][i] == '7' || mask[origin.y + 1][i] == 'F' || mask[origin.y + 1][i] == '-')
                        {
                            direction = 2;
                            continue;
                        }
                        if (mask[origin.y + 1][i] == 'I') mask.FloodMaze((origin.y + 1, i));
                    }
                    break;
                }
            }

            direction = 0;
            if (origin.x + 1 < mask[origin.y].Length)
            {
                // check if can squeeze right
                for (var i = origin.x + 1; i < mask[origin.y].Length; i++)
                {
                    if (mask[origin.y][i] == 'I') mask.FloodMaze((origin.y, i));
                    if (origin.y - 1 >= 0 && (direction == 0 || direction == 1) && (mask[origin.y][i] == '7' || mask[origin.y][i] == 'F' || mask[origin.y][i] == '-'))
                    {
                        if (mask[origin.y - 1][i] == 'L' || mask[origin.y - 1][i] == 'J' || mask[origin.y - 1][i] == '-')
                        {
                            direction = 1;
                            continue;
                        }
                        if (mask[origin.y - 1][i] == 'I') mask.FloodMaze((origin.y - 1, i));
                        break;
                    }
                    if (origin.y + 1 < mask.Length && (direction == 0 || direction == 2) && (mask[origin.y][i] == 'L' || mask[origin.y][i] == 'J' || mask[origin.y][i] == '-'))
                    {
                        if (mask[origin.y + 1][i] == '7' || mask[origin.y + 1][i] == 'F' || mask[origin.y + 1][i] == '-')
                        {
                            direction = 2;
                            continue;
                        }
                        if (mask[origin.y + 1][i] == 'I') mask.FloodMaze((origin.y + 1, i));
                    }
                    break;
                }
            }

            
        }

        private static void PrintMaskedMaze(this char[][] mask)
        {
            Console.OutputEncoding = Encoding.UTF8;

            foreach (var line in mask)
            {
                foreach (var c in line)
                {
                    if (c == 'I') Console.ForegroundColor = ConsoleColor.Green;
                    else if (c == 'O') Console.ForegroundColor = ConsoleColor.DarkRed;
                    else Console.ForegroundColor = ConsoleColor.White;

                    switch (c)
                    {
                        case 'F':
                            Console.Write("╔");
                            break;
                        case '7':
                            Console.Write("╗");
                            break;
                        case 'L':
                            Console.Write("╚");
                            break;
                        case 'J':
                            Console.Write("╝");
                            break;
                        case '|':
                            Console.Write("║");
                            break;
                        case '-':
                            Console.Write("═");
                            break;
                        default:
                            Console.Write(c);
                            break;
                    }

                }

                Console.WriteLine("");
            }
        }
    }
}
