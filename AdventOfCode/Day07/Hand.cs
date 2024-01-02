namespace AdventOfCode2023.Day07
{
    public enum HandType
    {
        HighCard = 1,
        OnePair,
        TwoPair,
        ThreeOfKind,
        FullHouse,
        FourOfKind,
        FiveOfKind
    }

    public interface IHand
    {
        int Bid { get; set; }
        string Cards { get; set; }
        HandType Type { get; set; }
        int[] CardStregth { get; set; }
    }

    public class Hand: IHand, IComparable
    {
        public int Bid { get; set; }
        public string Cards { get; set; }
        public HandType Type { get; set; }
        public int[] CardStregth { get; set; }

        public Hand(string info)
        {
            Bid = int.Parse(info.Split(" ")[1]);
            Cards = info.Split(" ")[0];

            CardStregth = new int[5];
            var diffrentLabels = new Dictionary<char, int>();
            for (var i = 0; i < Cards.Length; i++)
            {
                if (diffrentLabels.ContainsKey(Cards[i]))
                    diffrentLabels[Cards[i]] ++;
                else
                    diffrentLabels.Add(Cards[i], 1);

                switch (Cards[i])
                {
                    case 'A':
                        CardStregth[i] = 14;
                        break;
                    case 'K':
                        CardStregth[i] = 13;
                        break;
                    case 'Q':
                        CardStregth[i] = 12;
                        break;
                    case 'J':
                        CardStregth[i] = 11;
                        break;
                    case 'T':
                        CardStregth[i] = 10;
                        break;
                    default:
                        CardStregth[i] = Cards[i] - 48;
                        break;
                }
            }

            diffrentLabels = diffrentLabels.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            switch (diffrentLabels.Count)
            {
                case 1:
                    Type = HandType.FiveOfKind;
                    break;
                case 2:
                    Type = diffrentLabels.First().Value == 4 ? HandType.FourOfKind : HandType.FullHouse; 
                    break;
                case 3:
                    Type = diffrentLabels.First().Value == 3 ? HandType.ThreeOfKind : HandType.TwoPair;
                    break;
                case 4:
                    Type = HandType.OnePair;
                    break;
                case 5:
                    Type = HandType.HighCard;
                    break;
            }
        }

        public int CompareTo(object? obj)
        {
            var otherHand = obj as IHand;

            if ((int)Type > (int)otherHand.Type) return 1;
            if ((int)Type < (int)otherHand.Type) return -1;

            for (var i = 0; i < 5; i++)
            {
                if (CardStregth[i] == otherHand.CardStregth[i]) continue;
                if (CardStregth[i] > otherHand.CardStregth[i]) return 1;
                return -1;
            }
            return 0;
        }
    }

    public class JHand : IHand, IComparable
    {
        public int Bid { get; set; }
        public string Cards { get; set; }
        public HandType Type { get; set; }
        public int[] CardStregth { get; set; }

        public JHand(string info)
        {
            Bid = int.Parse(info.Split(" ")[1]);
            Cards = info.Split(" ")[0];

            CardStregth = new int[5];
            var diffrentLabels = new Dictionary<char, int>();
            for (var i = 0; i < Cards.Length; i++)
            {
                if (diffrentLabels.ContainsKey(Cards[i]))
                    diffrentLabels[Cards[i]]++;
                else
                    diffrentLabels.Add(Cards[i], 1);

                switch (Cards[i])
                {
                    case 'A':
                        CardStregth[i] = 14;
                        break;
                    case 'K':
                        CardStregth[i] = 13;
                        break;
                    case 'Q':
                        CardStregth[i] = 12;
                        break;
                    case 'J':
                        CardStregth[i] = 1;
                        break;
                    case 'T':
                        CardStregth[i] = 10;
                        break;
                    default:
                        CardStregth[i] = Cards[i] - 48;
                        break;
                }
            }

            diffrentLabels = diffrentLabels.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            if (Cards.Contains("J") && !Cards.Equals("JJJJJ"))
            {
                var majorLabel = diffrentLabels.First(x => x.Key != 'J').Key; // detect the highest amount of label other than J
                diffrentLabels[majorLabel] += diffrentLabels['J']; // conver J's to the majority label type
                diffrentLabels.Remove('J'); // after conversion remove J from the list
            }
            switch (diffrentLabels.Count)
            {
                case 1:
                    Type = HandType.FiveOfKind;
                    break;
                case 2:
                    Type = diffrentLabels.First().Value == 4 ? HandType.FourOfKind : HandType.FullHouse;
                    break;
                case 3:
                    Type = diffrentLabels.First().Value == 3 ? HandType.ThreeOfKind : HandType.TwoPair;
                    break;
                case 4:
                    Type = HandType.OnePair;
                    break;
                case 5:
                    Type = HandType.HighCard;
                    break;
            }
        }

        public int CompareTo(object? obj)
        {
            var otherHand = obj as IHand;

            if ((int)Type > (int)otherHand.Type) return 1;
            if ((int)Type < (int)otherHand.Type) return -1;

            for (var i = 0; i < 5; i++)
            {
                if (CardStregth[i] == otherHand.CardStregth[i]) continue;
                if (CardStregth[i] > otherHand.CardStregth[i]) return 1;
                return -1;
            }
            return 0;
        }
    }
} 
