namespace AdventOfCode2023.Day3
{
    public static class GearRatios
    {
        public static int FixEngine()
        {
            var engineSchema = File.ReadAllLines("Day3\\schema.txt");

            var engineData = new char[engineSchema.Length][];
            for(var i = 0; i < engineSchema.Length; i++)
                engineData[i] = engineSchema[i].ToCharArray();

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

        public static bool IsValidPartNumber(this char[][] schema, int i, int j, int length)
        {
            var start = j - length + 1;
            for(var k = start; k <= j; k++)
            {
                if (i - 1 > 0)
                {
                    // check top-left
                    if (k - 1 > 0)
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
                if(k - 1 > 0)
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
                    if (k - 1 > 0)
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
    }
}
