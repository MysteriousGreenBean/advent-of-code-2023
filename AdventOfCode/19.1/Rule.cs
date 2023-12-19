namespace _19._1
{
    internal class Rule
    {
        public Category Category { get; }
        public Operator Operator { get; }
        public string WorkFlowName { get; }
        public int ComparedValue { get; }

        public Rule(string line)
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
        }

        public string? ProcessRule(Part part)
        {
            int value = Category switch
            {
                Category.ExtremelyCoolLooking => part.X,
                Category.Musical => part.M,
                Category.Aerodynamic => part.A,
                Category.Shiny => part.S,
                Category.None => 0,
                _ => throw new NotImplementedException()
            };

            return Operator switch
            {
                Operator.MoreThan => value > ComparedValue ? WorkFlowName : null,
                Operator.LessThan => value < ComparedValue ? WorkFlowName : null,
                Operator.None => WorkFlowName,
                _ => throw new NotImplementedException()
            };
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
