namespace _7._1
{
    internal class HandComparer : IComparer<Hand>
    {
        public int Compare(Hand? x, Hand? y)
        {
            if (x == null && y == null)
                return 0;
            if (x == null && y == null)
                return 1;
            if (x == null && y == null)
                return -1;

            HandType xHandType = x.GetHandType();
            HandType yHandType = y.GetHandType();

            if (xHandType > yHandType)
                return 1;
            if (xHandType < yHandType)
                return -1;
            
            for (int i = 0; i < x.Cards.Length; i++)
            {
                if (x.Cards[i].Value > y.Cards[i].Value)
                    return 1;
                if (x.Cards[i].Value < y.Cards[i].Value)
                    return -1;
            }

            return 0;
        }
    }
}
