namespace _7._2
{
    internal class HandVariants
    {
        private readonly Hand originalHand;

        public HandVariants(Hand originalHand)
        {
            this.originalHand = originalHand;
        }

        public Hand GetVariantWithHighestType()
        {
            if (originalHand.Cards.All(card => card.Symbol == 'J'))
                return originalHand;

            Hand highestTypeHand = originalHand;
            foreach (Card nonJokerCard in originalHand.Cards.Where(card => card.Symbol != 'J').Distinct())
            {
                var handVariant = new Hand
                {
                    Cards = originalHand.Cards.Select(card => card.Symbol == 'J' ? new Card { Symbol = nonJokerCard.Symbol } : card).ToArray(),
                    Bet = originalHand.Bet
                };
                if (handVariant.GetHandType() > highestTypeHand.GetHandType())
                    highestTypeHand = handVariant;
            }

            originalHand.AdjustHandType(highestTypeHand.GetHandType());
            return originalHand;
        }
    }
}
