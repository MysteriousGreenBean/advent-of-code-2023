namespace _4._2
{
    internal class Card
    {
        public int[] WinningNumbers { get; }
        public int[] MarkedNumbers { get; }
        public int CopiesAmount { get; set; }

        public Card(string cardLine)
        {
            string[] cardParts = cardLine.Split(':', '|');
            WinningNumbers = cardParts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            MarkedNumbers = cardParts[2].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            CopiesAmount = 1;
        }

        public int GetScore()
        {
            int winningAmount = GetWonAmount();
            if (winningAmount == 0)
                return 0;
            return (int)Math.Pow(2, winningAmount - 1);
        }

        public int GetWonAmount()
            => WinningNumbers.Count(MarkedNumbers.Contains);
    }
}
