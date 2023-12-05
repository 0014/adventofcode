namespace AdventOfCode2023.Day3
{
    public static class GearRatios
    {
        public static int FixEngine()
        {
            var engineSchema = File.ReadAllLines("Day3\\schema.txt");

            //convert schema into char[][]
            var engineData = new char[engineSchema.Length][];
            for(var i = 0; i < engineSchema.Length; i++)
                engineData[i] = engineSchema[i].ToCharArray();

            //Detect engine parts 
            var sum = 0;
            for(var i = 0; i < engineData.Length; i++)
            {
                var partNumber = "";
                for(var j = 0; j < engineData[i].Length; j++)
                {
                    if (Char.IsDigit(engineData[i][j]))
                    {
                        partNumber += engineData[i][j];

                        if ((j == engineData.Length - 1 || !Char.IsDigit(engineData[i][j + 1])) && engineData.IsValidPartNumber(i, j, partNumber.Length))
                            sum += int.Parse(partNumber);
                    }
                    else partNumber = "";
                }
            }

            return sum;
        }

        private static bool IsValidPartNumber(this char[][] schema, int i, int j, int length)
        {
            var start = j - length + 1;
            for(var k = start; k <= j; k++)
            {
                if (i - 1 >= 0)
                {
                    // check top-left
                    if (k - 1 >= 0)
                    {
                        if (!Char.IsDigit(schema[i - 1][k - 1]) && schema[i - 1][k - 1] != '.')
                            return true;
                    }
                    // check top
                    if (!Char.IsDigit(schema[i - 1][k]) && schema[i - 1][k] != '.')
                        return true;
                    // check top-right
                    if(k + 1 < schema[i].Length)
                    {
                        if (!Char.IsDigit(schema[i - 1][k + 1]) && schema[i - 1][k + 1] != '.')
                            return true;
                    }
                    
                }
                //check left
                if(k - 1 >= 0)
                {
                    if (!Char.IsDigit(schema[i][k - 1]) && schema[i][k - 1] != '.')
                        return true;
                }
                // check right
                if(k + 1 < schema[i].Length)
                {
                    if (!Char.IsDigit(schema[i][k + 1]) && schema[i][k + 1] != '.')
                        return true;
                }

                if (i + 1 < schema.Length)
                {
                    // check bottom-left
                    if (k - 1 >= 0)
                    {
                        if (!Char.IsDigit(schema[i + 1][k - 1]) && schema[i + 1][k - 1] != '.')
                            return true;
                    }
                    // check bottom
                    if (!Char.IsDigit(schema[i + 1][k]) && schema[i + 1][k] != '.')
                        return true;
                    // check bottom-right
                    if (k + 1 < schema[i].Length)
                    {
                        if (!Char.IsDigit(schema[i + 1][k + 1]) && schema[i + 1][k + 1] != '.')
                            return true;
                    }
                }
            }

            return false;
        }

        public static int FindWrongGear()
        {
            var engine = new List<EnginePart>();
            var engineSchema = File.ReadAllLines("Day3\\schema.txt");
            
            //convert schema into char[][]
            var engineData = new char[engineSchema.Length][];
            for (var i = 0; i < engineSchema.Length; i++)
                engineData[i] = engineSchema[i].ToCharArray();
            
            //load all schema parts into the list of engine parts
            for (var i = 0; i < engineData.Length; i++)
            {
                var partNumber = "";
                for (var j = 0; j < engineData[i].Length; j++)
                {
                    if (Char.IsDigit(engineData[i][j]))
                    {
                        partNumber += engineData[i][j];

                        if (j == engineData.Length - 1 || !Char.IsDigit(engineData[i][j + 1]))
                        {
                            var coordinates = new List<(int i, int j)>();
                            for(var k = j-partNumber.Length+1;  k <= j; k++) coordinates.Add((i, k));
                            engine.Add(new EnginePart { PartNumber = partNumber, Coordinates = coordinates });
                        }
                    }
                    else partNumber = "";
                }
            }

            // detect wrong engine gear
            var sum = 0;
            for (var i = 0; i < engineData.Length; i++)
            {
                for (var j = 0; j < engineData[i].Length; j++)
                {
                    if (engineData[i][j] != '*') continue;

                    var coordinates = engineData.GetWrongGearCoordinates(i, j);
                    var suspiciousParts = new List<EnginePart>();
                    foreach (var coordinate in coordinates)
                    {
                        var part = engine.First(x => x.Coordinates.Contains(coordinate));
                        if (!suspiciousParts.Contains(part)) suspiciousParts.Add(part);
                    }

                    if (suspiciousParts.Count == 2)
                        sum += int.Parse(suspiciousParts[0].PartNumber) * int.Parse(suspiciousParts[1].PartNumber);
                }
            }

            return sum;
        }

        private static List<(int i, int j)> GetWrongGearCoordinates(this char[][] schema, int i, int j)
        {
            var coordinates = new List<(int i, int j)>();

            if (i - 1 >= 0)
            {
                // check top-left
                if (j - 1 >= 0)
                {
                    if (Char.IsDigit(schema[i - 1][j - 1]))
                        coordinates.Add((i - 1,j - 1));
                }
                // check top
                if (Char.IsDigit(schema[i - 1][j]))
                    coordinates.Add((i - 1, j));
                // check top-right
                if (j + 1 < schema[i].Length)
                {
                    if (Char.IsDigit(schema[i - 1][j + 1]))
                        coordinates.Add((i - 1, j + 1));
                }

            }
            //check left
            if (j - 1 >= 0)
            {
                if (Char.IsDigit(schema[i][j - 1]))
                    coordinates.Add((i, j - 1));
            }
            // check right
            if (j + 1 < schema[i].Length)
            {
                if (Char.IsDigit(schema[i][j + 1]))
                    coordinates.Add((i, j + 1));
            }

            if (i + 1 < schema.Length)
            {
                // check bottom-left
                if (j - 1 >= 0)
                {
                    if (Char.IsDigit(schema[i + 1][j - 1]))
                        coordinates.Add((i + 1, j - 1));
                }
                // check bottom
                if (Char.IsDigit(schema[i + 1][j]))
                    coordinates.Add((i + 1, j));
                // check bottom-right
                if (j + 1 < schema[i].Length)
                {
                    if (Char.IsDigit(schema[i + 1][j + 1]))
                        coordinates.Add((i + 1, j + 1));
                }
            }

            return coordinates;
        }

    }
}
