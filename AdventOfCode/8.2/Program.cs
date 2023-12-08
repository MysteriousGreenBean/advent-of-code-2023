using _8._2;

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

List<Node> currentNodes = nodes.Keys.Where(k => k.EndsWith("A")).Select(k => nodes[k]).ToList();
Node[] visitedZNodes = currentNodes.ToArray();
int[] visitedZNodesInstructionIndexes = new int[currentNodes.Count];
long steps = 0;
while (!visitedZNodes.All(n => n.NodeSymbol.EndsWith('Z')))
{
    for (int i = 0; i < currentNodes.Count; i++)
    {
        var currentNode = currentNodes[i];
        if (currentNode.NodeSymbol.EndsWith('Z'))
        {
            visitedZNodes[i] = currentNode;
            visitedZNodesInstructionIndexes[i] = currentInstructionIndex;
        }

        if (instructions[currentInstructionIndex] == 'L')
            currentNode = nodes[currentNode.LeftNodeSymbol];
        else
            currentNode = nodes[currentNode.RightNodeSymbol];

        currentNodes[i] = currentNode;

    }
    if (++currentInstructionIndex == instructions.Length)
        currentInstructionIndex = 0;
    steps++;
}

int[] stepsToNextZ = new int[currentNodes.Count];
for (int i = 0; i < visitedZNodes.Length; i++)
{
    Node node = visitedZNodes[i];
    int instructionIndex = visitedZNodesInstructionIndexes[i];
    do
    {
        if (instructions[instructionIndex] == 'L')
            node = nodes[node.LeftNodeSymbol];
        else
            node = nodes[node.RightNodeSymbol];
        stepsToNextZ[i]++;
        if (++instructionIndex == instructions.Length)
            instructionIndex = 0;
    }
    while (!node.NodeSymbol.EndsWith('Z'));
}

long lcmResult = stepsToNextZ[0];

for (long i = 1; i < stepsToNextZ.Length; i++)
{
    lcmResult = FindLCM(lcmResult, stepsToNextZ[i]);
}

Console.WriteLine(lcmResult);

long FindGCD(long a, long b)
{
    while (b != 0)
    {
        long temp = b;
        b = a % b;
        a = temp;
    }
    return a;
}

long FindLCM(long a, long b)
{
    return (a * b) / FindGCD(a, b);
}