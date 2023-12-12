namespace _12._2
{
    internal class Record
    {
        private string record;
        private int[] groups;

        public Record(string record)
        {
            string[] split = record.Split(' ');
            this.record = string.Join('?', Enumerable.Repeat(split[0], 5));
            groups = string.Join(',', Enumerable.Repeat(split[1], 5)).Split(',').Select(n => int.Parse(n)).ToArray();
        }


        public long GetMatchingCombinationsAmount()
        {
            return GetMatchingCombinationsAmount(0, 0, new Dictionary<(int, int), long>());
        }

        private long GetMatchingCombinationsAmount(int patternIndex, int countIndex, Dictionary<(int, int), long> memo)
        {
            var memoKey = (patternIndex, countIndex);
            if (memo.TryGetValue(memoKey, out long memoValue))
            {
                return memoValue;
            }

            if (patternIndex >= record.Length)
            {
                return countIndex == groups.Length ? 1 : 0;
            }

            long combinations = 0;

            var currentPatternChar = record[patternIndex];
            if (currentPatternChar != '#') // either . so we skip it, or ? so consider what if we do skip it
            {
                combinations += GetMatchingCombinationsAmount(patternIndex + 1, countIndex, memo);
            }

            if (currentPatternChar != '.' && countIndex < groups.Length) // either # so we consume it, or ? so consider what if we do consume it
            {
                var enoughToConsume = true;
                var neededCount = groups[countIndex] - 1; // we've already got the first
                while (enoughToConsume && neededCount > 0)
                {
                    patternIndex++;
                    neededCount--;
                    enoughToConsume = patternIndex < record.Length && record[patternIndex] != '.';
                }

                if (enoughToConsume)
                {
                    var separatorIndex = patternIndex + 1;
                    if (separatorIndex >= record.Length || record[separatorIndex] != '#')
                    {
                        combinations += GetMatchingCombinationsAmount(separatorIndex + 1, countIndex + 1, memo);
                    }
                }
            }

            memo.Add(memoKey, combinations);
            return combinations;
        }
    }
}
