namespace _14._1
{
    internal class Panel
    {
        private char[][] panelLines;
        public Panel(string[] panelLines) {
            this.panelLines = panelLines.Select(s => s.ToCharArray()).ToArray();
        }

        public void TiltNorth()
        {
            for (int i = 1; i < panelLines.Length; i++)
            {
                for (int j = 0; j < panelLines[0].Length; j++)
                {
                    if (panelLines[i][j] == 'O')
                    {
                        int northIndex = i - 1;
                        int currentIndex = i;
                        while (northIndex >= 0 && panelLines[northIndex][j] == '.')
                        {
                            panelLines[currentIndex--][j] = '.';
                            panelLines[northIndex--][j] = 'O';
                        }
                    }
                }
            }
        }

        public int CalculateLoad()
        {
            int multiplier = panelLines.Length;
            int load = 0;
            foreach (string line in panelLines.Select(c => new string(c)))
                load += line.Count(c => c == 'O') * multiplier--;
            return load;
        }

        public void Print()
        {
            foreach (var line in panelLines)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
