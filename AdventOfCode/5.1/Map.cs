namespace _5._1
{
    internal class Map
    {
        private List<Entry> entries;

        public Map(IEnumerable<string> inputLines)
        {
            entries = inputLines.Select(inputLine => inputLine.Split(' '))
                .Select(inputLineParts => new Entry
                {
                    DestinationRangeStart = long.Parse(inputLineParts[0]),
                    SourceRangeStart = long.Parse(inputLineParts[1]),
                    RangeLength = long.Parse(inputLineParts[2])
                })
                .ToList();
        }

        public long GetMappedValue(long sourceValue)
        {
            foreach (Entry entry in entries)
            {
                if (sourceValue >= entry.SourceRangeStart &&
                                       sourceValue < entry.SourceRangeStart + entry.RangeLength)
                    return entry.DestinationRangeStart + sourceValue - entry.SourceRangeStart;
            }
            return sourceValue;
        }

        private class Entry
        {
            public long DestinationRangeStart { get; init; }
            public long SourceRangeStart { get; init; }
            public long RangeLength { get; init; }
        }
    }
}
