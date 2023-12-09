namespace _9._2
{
    internal class Sequence
    {
        public int[] RecordedSequence { get; }

        public Sequence(int[] recordedSequence)
        {
            this.RecordedSequence = recordedSequence;
        }

        public int ExtrapolatePreviousNumber()
        {
            var extrapolationSequences = new List<int[]>();
            int[] currentExtrapolationSequence = CalculateNextExtrapolationSequence(this.RecordedSequence);
            extrapolationSequences.Add(this.RecordedSequence);
            extrapolationSequences.Add(currentExtrapolationSequence);
            while (!currentExtrapolationSequence.All(n => n == 0))
            {
                currentExtrapolationSequence = CalculateNextExtrapolationSequence(currentExtrapolationSequence);
                extrapolationSequences.Add(currentExtrapolationSequence);
            }

            return ExtrapolatePreviousNumber(extrapolationSequences);
        }

        private int[] CalculateNextExtrapolationSequence(int[] extrapolationSequence)
        {
            var result = new int[extrapolationSequence.Length - 1];
            for (int i = 1; i < extrapolationSequence.Length; i++)
                result[i - 1] = extrapolationSequence[i] - extrapolationSequence[i - 1];
            return result;
        }

        private int ExtrapolatePreviousNumber(List<int[]> extrapolationSequences)
        {
            for (int i = extrapolationSequences.Count - 1; i >= 0; i--)
            {
                extrapolationSequences[i] = extrapolationSequences[i].Prepend(ExtrapolatePreviousNumber(extrapolationSequences[i], i == extrapolationSequences.Count - 1 ? null : extrapolationSequences[i + 1])).ToArray();
            }
            return extrapolationSequences[0].First();
        }

        private int ExtrapolatePreviousNumber(int[] extrapolatedSequence, int[]? lowerExtrapolationSequence)
        {
            if (extrapolatedSequence.All(n => n == 0))
                return 0;

            if (lowerExtrapolationSequence == null)
                throw new ArgumentNullException(nameof(lowerExtrapolationSequence));

            return extrapolatedSequence.First() - lowerExtrapolationSequence.First();
        }
    }
}
