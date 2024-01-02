namespace AdventOfCode2023.Day08
{
    public static class HauntedWasteland
    {
        private const int Left = 0;
        private const int Right = 1;

        public static int NavigateWasteland()
        {
            var mapInfo = File.ReadAllLines("Day08\\map.txt");
            var directions = mapInfo[0];
            var map = new Dictionary<string, string[]>();
            for(var i = 2; i < mapInfo.Length; i++)
                map.Add(mapInfo[i].Substring(0, 3), new[] { mapInfo[i].Substring(7, 3), mapInfo[i].Substring(12, 3) });

            var location = "AAA";
            var counter = 0; var directionIndex = 0;
            while (location != "ZZZ")
            {
                var direction = directions[directionIndex];
                
                location = direction == 'L' ? map[location][Left] : map[location][Right];

                directionIndex ++;
                directionIndex %= directions.Length;
                counter ++;
            }

            return counter;
        }

        public static long NavigateWastelandAsGhost()
        {
            var mapInfo = File.ReadAllLines("Day08\\map.txt");
            var directions = mapInfo[0];
            var map = new Dictionary<string, string[]>();
            for (var i = 2; i < mapInfo.Length; i++)
                map.Add(mapInfo[i].Substring(0, 3), new[] { mapInfo[i].Substring(7, 3), mapInfo[i].Substring(12, 3) });

            var locations = map
                .Where(x => x.Key.EndsWith("A"))
                .Select(x => x.Key)
                .ToList();
            var counter = new int[locations.Count]; 
            for (var i = 0; i < locations.Count; i++)
            {
                var directionIndex = 0;
                while (!locations[i].EndsWith("Z"))
                {
                    var direction = directions[directionIndex];

                    locations[i] = direction == 'L' ? map[locations[i]][Left] : map[locations[i]][Right];

                    directionIndex++;
                    directionIndex %= directions.Length;
                    counter[i]++;
                }
            }

            long lcm = counter[0];
            for (var i = 1; i < counter.Length; i++)
                lcm = LeastCommonMultiple(lcm, counter[i]);
            return lcm;
        }

        private static long LeastCommonMultiple(long a, long b)
        {
            return (a / GreatestCommonFactor(a, b)) * b;
        }

        private static long GreatestCommonFactor(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}
