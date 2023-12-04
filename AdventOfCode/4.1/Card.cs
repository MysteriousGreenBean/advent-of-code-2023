namespace _4._1
{
    internal class Card
    {
        public int[] WinningNumbers { get; }
        public int[] MarkedNumbers { get; }

        public Card(string cardLine)
        {
            string[] cardParts = cardLine.Split(':', '|');
            WinningNumbers = cardParts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            MarkedNumbers = cardParts[2].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        }

        public int GetScore()
        {
            int winningAmount = WinningNumbers.Count(MarkedNumbers.Contains);
            if (winningAmount == 0)
                return 0;
            return (int)Math.Pow(2, winningAmount - 1);
        }
    }
}
