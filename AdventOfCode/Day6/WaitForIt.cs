namespace AdventOfCode2023.Day6
{
    public static class WaitForIt
    {
        public static long ErrorMarginInBoatRacing()
        {
            var limits = File.ReadAllLines("Day6\\limits.txt");
            var timeLimits = Array.ConvertAll(limits[0].Substring(5).Trim().Split(" ").Where(x => !String.IsNullOrEmpty(x)).ToArray(), long.Parse);
            var distanceLimits = Array.ConvertAll(limits[1].Substring(9).Trim().Split(" ").Where(x => !String.IsNullOrEmpty(x)).ToArray(), long.Parse);

            return CalculateErrorMarginInBoatRacing(timeLimits, distanceLimits);
        }

        public static long ErrorMarginInBoatRacingWithCorrectedSheet()
        {
            var limits = File.ReadAllLines("Day6\\limits.txt");
            var timeLimit = Int64.Parse(limits[0].Substring(5).Replace(" ", ""));
            var distanceLimit = Int64.Parse(limits[1].Substring(9).Replace(" ", ""));

            return CalculateErrorMarginInBoatRacing(new []{ timeLimit }, new[] { distanceLimit });
        }

        private static long CalculateErrorMarginInBoatRacing(long[] timeLimits, long[] distanceLimits)
        {
            long errorMargin = 1;
            for (var i = 0; i < timeLimits.Length; i++)
            {
                var countPotentialWins = 0;
                var timeLimit = timeLimits[i];
                var distanceLimit = distanceLimits[i];

                for (var holdingTime = 1; holdingTime < timeLimit; holdingTime++)
                    if ((timeLimit - holdingTime) * holdingTime > distanceLimit) countPotentialWins++;

                errorMargin *= countPotentialWins;
            }

            return errorMargin;
        }
    }
}
