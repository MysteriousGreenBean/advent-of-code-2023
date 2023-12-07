using System.Diagnostics;

namespace _7._2
{
    [DebuggerDisplay("{Cards[0].Symbol}{Cards[1].Symbol}{Cards[2].Symbol}{Cards[3].Symbol}{Cards[4].Symbol} {Bet}")]
    internal class Hand
    {
        private HandType handType;

        public Card[] Cards { get; init; }

        public int Bet { get; init; }

        public void AdjustHandType(HandType handType)
        {
            this.handType = handType;
        }

        public HandType GetHandType()
        {
            if (handType != 0)
                return handType;

            if (Cards.Length != 5)
                throw new InvalidOperationException("Hand must consist of 5 cards");

            if (Cards.GroupBy(card => card.Symbol).Count() == 1)
                return HandType.FiveOfKind;

            if (Cards.GroupBy(card => card.Symbol).Count() == 2)
            {
                if (Cards.GroupBy(card => card.Symbol).Any(group => group.Count() == 4))
                    return HandType.FourOfKind;
                else
                    return HandType.FullHouse;
            }

            if (Cards.GroupBy(card => card.Symbol).Count() == 3)
            {
                if (Cards.GroupBy(card => card.Symbol).Any(group => group.Count() == 3))
                    return HandType.ThreeOfKind;
                else
                    return HandType.TwoPairs;
            }

            if (Cards.GroupBy(card => card.Symbol).Count() == 4)
                return HandType.Pair;

            return HandType.HighCard;
        }
    }

    internal enum HandType
    {
        FiveOfKind = 7,
        FourOfKind = 6,
        FullHouse = 5,
        ThreeOfKind = 4,
        TwoPairs = 3,
        Pair = 2,
        HighCard = 1
    }
}
