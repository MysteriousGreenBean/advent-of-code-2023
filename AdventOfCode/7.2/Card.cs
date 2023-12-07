using System.Diagnostics;

namespace _7._2
{
    [DebuggerDisplay("{Symbol}")]
    internal class Card
    {
        private static readonly Dictionary<char, int> cardValues = new()
        {
            { '2', 2 },
            { '3', 3 },
            { '4', 4 },
            { '5', 5 },
            { '6', 6 },
            { '7', 7 },
            { '8', 8 },
            { '9', 9 },
            { 'T', 10 },
            { 'J', 1 },
            { 'Q', 12 },
            { 'K', 13 },
            { 'A', 14 }
        };

        public char Symbol { get; init; }
        public int Value => cardValues[Symbol];
    }
}
