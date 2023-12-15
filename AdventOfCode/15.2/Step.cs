namespace _15._2
{
    internal class Step
    {
        private readonly string lensLabel;
        private readonly int lensFocalLength = -1;
        private readonly OperationType operationType;
        
        public Step(string stepInstruction)
        {
            this.operationType = stepInstruction.Contains('-') ? OperationType.RemoveLens : OperationType.AddLens;
            this.lensLabel = stepInstruction.Split(new char[] { '-', '=' }, StringSplitOptions.RemoveEmptyEntries)[0];
            if (operationType == OperationType.AddLens)
                this.lensFocalLength = int.Parse(stepInstruction.Split(new char[] { '=' })[1]);
        }

        public void Execute(Box[] boxes)
        {
            int labelHash = GetLabelHash();
            Lens? existingLens = boxes[labelHash].Lenses.FirstOrDefault(lens => lens.Label == lensLabel);
            if (operationType == OperationType.AddLens)
            {
                if (!existingLens.Value.Equals(default(Lens)))
                {
                    int indexOfExistingLens = boxes[labelHash].Lenses.IndexOf(existingLens.Value);
                    boxes[labelHash].Lenses[indexOfExistingLens] = new Lens(lensLabel, lensFocalLength);
                }
                else
                    boxes[labelHash].Lenses.Add(new Lens(lensLabel, lensFocalLength));
            }
            else
            {
                if (!existingLens.Value.Equals(default(Lens)))
                    boxes[labelHash].Lenses.Remove(existingLens.Value);
            }
        }

        private int GetLabelHash()
        {
            int currentValue = 0;
            foreach (char c in lensLabel)
            {
                currentValue += c;
                currentValue *= 17;
                currentValue = currentValue % 256;
            }
            return currentValue;
        }

        private enum OperationType
        {
            RemoveLens,
            AddLens
        }
    }
}
