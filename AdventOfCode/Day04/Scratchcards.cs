namespace AdventOfCode2023.Day04
{
    public static class Scratchcards
    {
        public static int CalculateTotalPoints()
        {
            var cards = File.ReadAllLines("Day04\\cards.txt");

            var sum = 0;
            foreach (var card in cards)
            {
                var cardInfo = card.Split(": ")[1];
                var winningNumbers = cardInfo.Split("|")[0].Trim().Split(' ');
                var elfsNumbers = cardInfo.Split("|")[1].Trim().Split(' ').ToList();

                var count = 0;
                foreach (var winningNumber in winningNumbers.Where(x => !String.IsNullOrEmpty(x)))
                    if (elfsNumbers.Contains(winningNumber)) count++;
                
                if(count > 0)  sum += (int) Math.Pow(2, count - 1);
            }

            return sum;
        }

        public static int CountCardsWon()
        {
            var cards = File.ReadAllLines("Day04\\cards.txt");

            var totalCards = new int[cards.Length];
            Array.Fill(totalCards, 1);

            for (var i = 0; i < cards.Length; i++)
            {
                var cardInfo = cards[i].Split(": ")[1];
                var winningNumbers = cardInfo.Split("|")[0].Trim().Split(' ');
                var elfsNumbers = cardInfo.Split("|")[1].Trim().Split(' ').ToList();

                var count = 0;
                foreach (var winningNumber in winningNumbers.Where(x => !String.IsNullOrEmpty(x)))
                    if (elfsNumbers.Contains(winningNumber)) count++;

                for (var j = i + 1; j <= i + count; j++)
                    totalCards[j] += totalCards[i];
            }

            return totalCards.Sum();
        }
    }
}
