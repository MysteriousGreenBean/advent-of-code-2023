namespace _19._2
{
    internal class Rule
    {
        public int WorkFlowIndex { get; }
        public int RuleIndex { get; }
        public Category Category { get; }
        public Operator Operator { get; }
        public string WorkFlowName { get; }
        public int ComparedValue { get; }

        public Rule(string line, int workFlowIndex, int ruleIndex)
        {
            string[] parts = line.Split(':');
            if (parts.Length > 1)
            {
                Category = (Category)parts[0][0];
                Operator = (Operator)parts[0][1];
                ComparedValue = int.Parse(parts[0][2..]);
                WorkFlowName = parts[1];
            }
            else
            {
                Category = Category.None;
                Operator = Operator.None;
                ComparedValue = 0;
                WorkFlowName = parts[0];
            }
            WorkFlowIndex = workFlowIndex;
            RuleIndex = ruleIndex;
        }

        public override string ToString()
        {
            if (Category == Category.None)
                return WorkFlowName;

            return $"{(char)Category}{(char)Operator}{ComparedValue}:{WorkFlowName}";
        }
    }

    internal enum Category
    {
        ExtremelyCoolLooking = 'x',
        Musical = 'm',
        Aerodynamic = 'a',
        Shiny = 's',
        None = 'j'
    }

    internal enum Operator
    {
        None = 'n',
        MoreThan = '>',
        LessThan = '<'
    }
}
