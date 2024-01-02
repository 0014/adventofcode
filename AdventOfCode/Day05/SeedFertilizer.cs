namespace AdventOfCode2023.Day05
{
    public static class SeedFertilizer
    {
        private static readonly string[] MapData = File.ReadAllLines("Day05\\map.txt");
        private static long SeedToSoil(this long source) => source.Map(MapData.GetMap("seed-to-soil map"));
        private static long SoilToFertilizer(this long source) => source.Map(MapData.GetMap("soil-to-fertilizer map"));
        private static long FertilizerToWater(this long source) => source.Map(MapData.GetMap("fertilizer-to-water map"));
        private static long WaterToLight(this long source) => source.Map(MapData.GetMap("water-to-light map"));
        private static long LightToTemperature(this long source) => source.Map(MapData.GetMap("light-to-temperature map"));
        private static long TemperatureToHumidity(this long source) => source.Map(MapData.GetMap("temperature-to-humidity map"));
        private static long HumidityToLocation(this long source) => source.Map(MapData.GetMap("humidity-to-location map"));

        public static long PlantSeeds()
        {
            var seeds = Array.ConvertAll(MapData[0].Substring(7).Split(' '), Int64.Parse);

            return seeds.GetMinimumLocation();
        }

        public static long PlantRangeOfSeeds()
        {
            var seedsParameters = Array.ConvertAll(MapData[0].Substring(7).Split(' '), Int64.Parse);

            var minLocation = 9999999999;
            for (var i = 0; i < seedsParameters.Length; i++)
            {
                if (i % 2 != 1) continue;

                for (var j = seedsParameters[i - 1]; j < seedsParameters[i - 1] + seedsParameters[i]; j++)
                {
                    var location = j
                        .SeedToSoil()
                        .SoilToFertilizer()
                        .FertilizerToWater()
                        .WaterToLight()
                        .LightToTemperature()
                        .TemperatureToHumidity()
                        .HumidityToLocation();

                    if (location < minLocation)
                    {
                        Console.WriteLine(location);
                        minLocation = location;
                    }
                }
            }

            return minLocation;
        }

        private static long GetMinimumLocation(this long[] seeds)
        {
            var minLocation = 9999999999;
            foreach (var seed in seeds)
            {
                var location = seed
                    .SeedToSoil()
                    .SoilToFertilizer()
                    .FertilizerToWater()
                    .WaterToLight()
                    .LightToTemperature()
                    .TemperatureToHumidity()
                    .HumidityToLocation();

                if (location < minLocation) minLocation = location;
            }

            return minLocation;
        }

        private static List<long[]> GetMap(this string[] mapData, string mapType)
        {
            var map = new List<long[]>();
            var readFlag = false;
            foreach (var line in mapData)
            {
                if (line.Contains(mapType))
                {
                    readFlag = true;
                    continue;
                }

                if (!readFlag) continue;
                if (String.IsNullOrEmpty(line)) return map;
                var mapParameters = Array.ConvertAll(line.Split(' '), Int64.Parse);
                map.Add(mapParameters);
            }

            return map;
        }

        private static long Map(this long source, List<long[]> map)
        {
            foreach (var mapParameters in map)
            {
                if (source >= mapParameters[1] && source < mapParameters[1] + mapParameters[2])
                    return mapParameters[0] + source - mapParameters[1];
            }
            
            return source;
        }
    }
}
