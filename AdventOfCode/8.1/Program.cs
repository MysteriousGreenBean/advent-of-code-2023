using _8._1;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);


var nodes = new Dictionary<string, Node>();
for (int i = 2; i < inputLines.Length; i++)
{
    var node = new Node(inputLines[i]);
    nodes.Add(node.NodeSymbol, node);
}
string instructions = inputLines[0];
int currentInstructionIndex = 0;
Node currentNode = nodes["AAA"];
int steps = 0;
while (currentNode.NodeSymbol != "ZZZ")
{
    if (instructions[currentInstructionIndex++] == 'L')
        currentNode = nodes[currentNode.LeftNodeSymbol];
    else
        currentNode = nodes[currentNode.RightNodeSymbol];
    if (currentInstructionIndex == instructions.Length)
        currentInstructionIndex = 0;
    steps++;
}

Console.WriteLine(steps);