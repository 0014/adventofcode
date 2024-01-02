namespace AdventOfCode2023.Day01
{
    public static class Trebuchet
    {
        public static int CalibrationValue()
        {
            var calibrationValues = File.ReadAllLines("Day01\\calibration-document.txt");

            var sum = 0;
            foreach (var calibrationValue in calibrationValues)
            {
                int firstDigit = -1, lastDigit = -1;
                foreach(var character in calibrationValue)
                {
                    if (!Char.IsDigit(character)) continue;

                    if(firstDigit == -1) firstDigit = character - 48; 
                    lastDigit = character - 48;
                }
                sum += firstDigit * 10 + lastDigit;
            }

            return sum;
        }

        public static int CorrectedCalibrationValue()
        {
            var calibrationValues = File.ReadAllLines("Day01\\calibration-document.txt");

            var sum = 0; var firstDigit = 0; var lastDigit = 0;
            foreach (var calibrationValue in calibrationValues)
            {
                var firstOccurances = new Dictionary<string, int>
                {
                    ["1"] = calibrationValue.IndexOf("1"),
                    ["2"] = calibrationValue.IndexOf("2"),
                    ["3"] = calibrationValue.IndexOf("3"),
                    ["4"] = calibrationValue.IndexOf("4"),
                    ["5"] = calibrationValue.IndexOf("5"),
                    ["6"] = calibrationValue.IndexOf("6"),
                    ["7"] = calibrationValue.IndexOf("7"),
                    ["8"] = calibrationValue.IndexOf("8"),
                    ["9"] = calibrationValue.IndexOf("9"),
                    ["one"] = calibrationValue.IndexOf("one"),
                    ["two"] = calibrationValue.IndexOf("two"),
                    ["three"] = calibrationValue.IndexOf("three"),
                    ["four"] = calibrationValue.IndexOf("four"),
                    ["five"] = calibrationValue.IndexOf("five"),
                    ["six"] = calibrationValue.IndexOf("six"),
                    ["seven"] = calibrationValue.IndexOf("seven"),
                    ["eight"] = calibrationValue.IndexOf("eight"),
                    ["nine"] = calibrationValue.IndexOf("nine")
                };

                var lastOccurances = new Dictionary<string, int>
                {
                    ["1"] = calibrationValue.LastIndexOf("1"),
                    ["2"] = calibrationValue.LastIndexOf("2"),
                    ["3"] = calibrationValue.LastIndexOf("3"),
                    ["4"] = calibrationValue.LastIndexOf("4"),
                    ["5"] = calibrationValue.LastIndexOf("5"),
                    ["6"] = calibrationValue.LastIndexOf("6"),
                    ["7"] = calibrationValue.LastIndexOf("7"),
                    ["8"] = calibrationValue.LastIndexOf("8"),
                    ["9"] = calibrationValue.LastIndexOf("9"),
                    ["one"] = calibrationValue.LastIndexOf("one"),
                    ["two"] = calibrationValue.LastIndexOf("two"),
                    ["three"] = calibrationValue.LastIndexOf("three"),
                    ["four"] = calibrationValue.LastIndexOf("four"),
                    ["five"] = calibrationValue.LastIndexOf("five"),
                    ["six"] = calibrationValue.LastIndexOf("six"),
                    ["seven"] = calibrationValue.LastIndexOf("seven"),
                    ["eight"] = calibrationValue.LastIndexOf("eight"),
                    ["nine"] = calibrationValue.LastIndexOf("nine")
                };

                firstDigit = GetDigit(firstOccurances.Where(x => x.Value != -1).MinBy(x => x.Value).Key);
                lastDigit = GetDigit(lastOccurances.MaxBy(x => x.Value).Key);
                sum += firstDigit * 10 + lastDigit;
            }
            return sum;
        }

        static int GetDigit(string s)
        {
            switch (s)
            {
                case "1":
                case "one":
                    return 1;
                case "2":
                case "two":
                    return 2;
                case "3":
                case "three":
                    return 3;
                case "4":
                case "four":
                    return 4;
                case "5":
                case "five":
                    return 5;
                case "6":
                case "six":
                    return 6;
                case "7":
                case "seven":
                    return 7;
                case "8":
                case "eight":
                    return 8;
                case "9":
                case "nine":
                    return 9;
            }
            return -1;
        }
    }
}
