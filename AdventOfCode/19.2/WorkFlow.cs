namespace _19._2
{
    internal class WorkFlow
    {
        public int Index { get; }
        public string Name { get; }
        public Rule[] Rules { get; }

        public WorkFlow(string line, int index)
        {
            string[] parts = line.Trim('}').Split(new char[] { '{', ',' }, StringSplitOptions.RemoveEmptyEntries);
            Name = parts[0];
            Rules = new Rule[parts.Length - 1];
            Index = index;
            for (int i = 1; i < parts.Length; i++)
                Rules[i - 1] = new Rule(parts[i], Index, i - 1);
        }

        public override string ToString()
        {
            return $"{Name}{{{string.Join(',', Rules.Select(r => r.ToString()))}}}";
        }
    }

    internal class WorkFlowProcessResult
    {
        public required WorkFlowProcessResultType Result { get; init; }
        public required string? NextWorkFlowName { get; init; }
    }

    internal enum WorkFlowProcessResultType
    {
        Accepted,
        Rejected,
        SentToNextWorkFlow
    }
}
