namespace _8._2
{
    internal class Node
    {
        public string NodeSymbol { get; }
        public string LeftNodeSymbol { get; }
        public string RightNodeSymbol { get; }

        public Node(string nodeLine)
        {
            string[] nodeLineParts = nodeLine.Split(new char[] { ' ', '=', ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            NodeSymbol = nodeLineParts[0];
            LeftNodeSymbol = nodeLineParts[1];
            RightNodeSymbol = nodeLineParts[2];
        }

        public override string ToString()
        {
            return $"{NodeSymbol} ({LeftNodeSymbol}, {RightNodeSymbol})";
        }
    }
}
