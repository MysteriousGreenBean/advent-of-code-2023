namespace _14._2
{
    internal class Panel
    {
        private List<char[][]> history = new List<char[][]>();
        private List<int> historyLoads = new List<int>();
        private char[][] panelLines;

        int cycleStart = -1;
        int cycleLength = -1;

        public Panel(string[] panelLines) {
            this.panelLines = panelLines.Select(s => s.ToCharArray()).ToArray();
        }

        public void FindCycle()
        {
            (int cycleStart, int cycleLength) = (-1, -1);
            while (cycleStart == -1 && cycleLength == -1)
            {
                CopyPanelLinesToHistory();
                TiltNorth();
                TiltWest();
                TiltSouth();
                TiltEast();
                (cycleStart, cycleLength) = CalculateCycle();
            }
            this.cycleStart = cycleStart;
            this.cycleLength = cycleLength;
        }

        public void MoveByCycles(int cycles)
        {
            if (cycleStart == -1 && cycleLength == -1)
                throw new InvalidOperationException("Cycle has not been found yet");

            int cyclesLeft = cycles % (cycleLength + 1);
            panelLines = history[cycleStart + cyclesLeft - 1];
        }

        private void CopyPanelLinesToHistory()
        {
            char[][] previousPanelLines = new char[panelLines.Length][];
            for (int i = 0; i < panelLines.Length; i++)
            {
                previousPanelLines[i] = new char[panelLines[0].Length];
                for (int j = 0; j < panelLines[0].Length; j++)
                {
                    previousPanelLines[i][j] = panelLines[i][j];
                }
            }
            history.Add(previousPanelLines);
            historyLoads.Add(CalculateLoad());
        }

        private (int cycleStart, int cycleLength) CalculateCycle()
        {
            for (int i = 0; i < history.Count - 1; i++)
            {
                if (ArePanelLinesEqual(history[i]))
                    return (i, history.Count - i - 1);
            }
            return (-1, -1);
        }

        private bool ArePanelLinesEqual(char[][] previousPanelLines)
        {
            for (int i = 0; i < panelLines.Length; i++)
            {
                for (int j = 0; j < panelLines[0].Length; j++)
                {
                    if (panelLines[i][j] != previousPanelLines[i][j])
                        return false;
                }
            }
            return true;
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

        public void TiltSouth()
        {
            for (int i = panelLines.Length - 2; i >= 0; i--)
            {
                for (int j = 0; j < panelLines[0].Length; j++)
                {
                    if (panelLines[i][j] == 'O')
                    {
                        int southIndex = i + 1;
                        int currentIndex = i;
                        while (southIndex < panelLines.Length && panelLines[southIndex][j] == '.')
                        {
                            panelLines[currentIndex++][j] = '.';
                            panelLines[southIndex++][j] = 'O';
                        }
                    }
                }
            }
        }

        public void TiltWest()
        {
            for (int i = 0; i < panelLines.Length; i++)
            {
                for (int j = 1; j < panelLines[0].Length; j++)
                {
                    if (panelLines[i][j] == 'O')
                    {
                        int westIndex = j - 1;
                        int currentIndex = j;
                        while (westIndex >= 0 && panelLines[i][westIndex] == '.')
                        {
                            panelLines[i][currentIndex--] = '.';
                            panelLines[i][westIndex--] = 'O';
                        }
                    }
                }
            }
        }

        public void TiltEast()
        {
            for (int i = 0; i < panelLines.Length; i++)
            {
                for (int j = panelLines[0].Length - 2; j >= 0; j--)
                {
                    if (panelLines[i][j] == 'O')
                    {
                        int eastIndex = j + 1;
                        int currentIndex = j;
                        while (eastIndex < panelLines[0].Length && panelLines[i][eastIndex] == '.')
                        {
                            panelLines[i][currentIndex++] = '.';
                            panelLines[i][eastIndex++] = 'O';
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
