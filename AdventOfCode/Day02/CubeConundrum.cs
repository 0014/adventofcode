namespace AdventOfCode2023.Day02
{
    public static class CubeConundrum
    {
        static readonly int MAX_NUMBER_OF_RED = 12;
        static readonly int MAX_NUMBER_OF_GREEN = 13;
        static readonly int MAX_NUMBER_OF_BLUE = 14;

        public static int PlayGame()
        {
            var gameList = File.ReadAllLines("Day02\\games.txt");

            var sum = 0;
            for(var i = 1; i < gameList.Length + 1; i++)
            {
                var isPossible = true;
                var gameInfo = gameList[i - 1].Split(": ")[1].Replace(" ", "");
                var rounds = gameInfo.Split(';');
                foreach(var round in rounds)
                {
                    var cubes = round.Split(',');
                    foreach(var cube in cubes)
                    {
                        if (cube.Contains("green"))
                        {
                            var n = int.Parse(cube.Replace("green", ""));
                            if (n > MAX_NUMBER_OF_GREEN) isPossible = false;
                        }
                        else if (cube.Contains("blue"))
                        {
                            var n = int.Parse(cube.Replace("blue", ""));
                            if (n > MAX_NUMBER_OF_BLUE) isPossible = false;
                        }
                        else
                        {
                            var n = int.Parse(cube.Replace("red", ""));
                            if (n > MAX_NUMBER_OF_RED) isPossible = false;
                        }
                    }
                }
                if (isPossible) sum += i;
            }

            return sum;
        }

        public static int PowerOfMinimumSet()
        {
            var gameList = File.ReadAllLines("Day02\\games.txt");

            var sum = 0;
            for (var i = 1; i < gameList.Length + 1; i++)
            {
                var gameInfo = gameList[i - 1].Split(": ")[1].Replace(" ", "");
                var rounds = gameInfo.Split(';');
                var minGreen = 0; var minBlue = 0; var minRed = 0;
                foreach (var round in rounds)
                {
                    var cubes = round.Split(',');
                    foreach (var cube in cubes)
                    {
                        if (cube.Contains("green"))
                        {
                            var n = int.Parse(cube.Replace("green", ""));
                            if (n > minGreen) minGreen = n;
                        }
                        else if (cube.Contains("blue"))
                        {
                            var n = int.Parse(cube.Replace("blue", ""));
                            if (n > minBlue) minBlue = n;
                        }
                        else
                        {
                            var n = int.Parse(cube.Replace("red", ""));
                            if (n > minRed) minRed = n;
                        }
                    }
                }
                sum += minGreen * minBlue * minRed;
            }

            return sum;
        }
    }
}
