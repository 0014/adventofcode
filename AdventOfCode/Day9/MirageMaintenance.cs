namespace AdventOfCode2023.Day9
{
    public static class MirageMaintenance
    {
        public static int ExtrapolateOasisData()
        {
            var historicalData = File.ReadAllLines("Day9\\data.txt");

            return historicalData.Sum(line => BuildData(line).Extrapolate());
        }

        public static int ExtrapolateBackwardsOasisData()
        {
            var historicalData = File.ReadAllLines("Day9\\data.txt");

            return historicalData.Sum(line => BuildData(line).ExtrapolateBackwards());
        }

        private static List<int[]> BuildData(string historicData)
        {
            var data = new List<int[]> { Array.ConvertAll(historicData.Split(" "), int.Parse) };
            var latest = data.Last();
            while (latest.Any(x => x != 0))
            {
                var newLine = new int[latest.Length - 1];
                for (var i = 0; i < latest.Length - 1; i++)
                    newLine[i] = latest[i + 1] - latest[i];

                latest = newLine;
                data.Add(newLine);
            }

            return data;
        }

        private static int Extrapolate(this List<int[]> dataset)
        {
            dataset.Reverse();

            var result = 0;
            foreach (var data in dataset)
                result += data[data.Length - 1];

            return result;
        }

        private static int ExtrapolateBackwards(this List<int[]> dataset)
        {
            dataset.Reverse();

            var result = 0;
            foreach (var data in dataset)
                result = data[0] - result;

            return result;
        }
    }
}
