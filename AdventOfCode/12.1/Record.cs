namespace _12._1
{
    internal class Record
    {
        private string record;
        private int[] groups;

        public Record(string record)
        {
            string[] split = record.Split(' ');
            this.record = split[0];
            groups = split[1].Split(',').Select(n => int.Parse(n)).ToArray();
        }

        public int GetMatchingCombinationsAmount()
        {
            List<int> questionMarkIndexes = GetAllQuestionMarkIndexes();
            int matchingCombinationsAmount = 0;
            GetCombinations(questionMarkIndexes).AsParallel().ForAll(combination =>
            {
            if (IsValid(combination))
                Interlocked.Increment(ref matchingCombinationsAmount);
            });
            return matchingCombinationsAmount;
        }

        private bool IsValid(string newRecord)
        {
            string[] newGroups = newRecord.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (newGroups.Length != groups.Length)
                return false;

            for (int i = 0; i < groups.Length; i++)
            {
                if (newGroups[i].Length != groups[i])
                    return false;
            }
            return true;
        }

        private List<int> GetAllQuestionMarkIndexes()
            => record.Select((c, i) => new { c, i }).Where(x => x.c == '?').Select(x => x.i).ToList();

        private IEnumerable<string> GetCombinations(List<int> questionMarkIndexes)
        {
            int combinationsCount = (int)Math.Pow(2, questionMarkIndexes.Count);

            for (int i = 0; i < combinationsCount; i++)
            {
                char[] combination = record.ToCharArray();
                for (int j = 0; j < questionMarkIndexes.Count; j++)
                {
                    if ((i & (1 << j)) != 0)
                        combination[questionMarkIndexes[j]] = '#';
                    else
                        combination[questionMarkIndexes[j]] = '.';
                }
                yield return new string(combination);
            }
        }
    }
}
