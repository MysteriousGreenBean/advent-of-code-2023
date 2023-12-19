namespace _19._1
{
    internal class WorkFlow
    {
        public string Name { get; }
        public Rule[] Rules { get; }

        public WorkFlow(string line)
        {
            string[] parts = line.Trim('}').Split(new char[] { '{', ',' }, StringSplitOptions.RemoveEmptyEntries);
            Name = parts[0];
            Rules = new Rule[parts.Length - 1];
            for (int i = 1; i < parts.Length; i++)
                Rules[i - 1] = new Rule(parts[i]);
        }

        public WorkFlowProcessResult ProcessWorkFlow(Part part)
        {
            foreach (Rule rule in Rules)
            {
                string? nextWorkFlowName = rule.ProcessRule(part);
                if (nextWorkFlowName == "A")
                    return new WorkFlowProcessResult { Result = WorkFlowProcessResultType.Accepted, NextWorkFlowName = null };
                if (nextWorkFlowName == "R")
                    return new WorkFlowProcessResult { Result = WorkFlowProcessResultType.Rejected, NextWorkFlowName = null };
                if (nextWorkFlowName != null)
                    return new WorkFlowProcessResult
                    {
                        Result = WorkFlowProcessResultType.SentToNextWorkFlow,
                        NextWorkFlowName = nextWorkFlowName
                    };
            }

            throw new InvalidDataException("No rule matched");
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
