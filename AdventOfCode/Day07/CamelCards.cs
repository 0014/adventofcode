namespace AdventOfCode2023.Day07
{
    public static class CamelCards
    {
        private static string[] HandsInfo => File.ReadAllLines("Day07\\hands.txt");

        public static int TotalWinnings()
        {
            var hands = HandsInfo.Select(handInfo => new Hand(handInfo)).Cast<IHand>().ToList();
            return hands.OrderBy(x => x).ToList().CalculateTotalWinnings();
        }

        public static int TotalWinningsWithJoker()
        {
            var hands = HandsInfo.Select(handInfo => new JHand(handInfo)).Cast<IHand>().ToList();
            return hands.OrderBy(x => x).ToList().CalculateTotalWinnings();
        }

        private static int CalculateTotalWinnings(this List<IHand> hands)
        {
            var total = 0;
            for (var i = 1; i <= hands.Count; i++) total += i * hands[i - 1].Bid;
            return total;
        }
    }
}
