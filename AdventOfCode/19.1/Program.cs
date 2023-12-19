using _19._1;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);

var workFlows = new List<WorkFlow>();
var approvedParts = new List<Part>();

int line = 0;

while (!string.IsNullOrEmpty(inputLines[line]))
{
    workFlows.Add(new WorkFlow(inputLines[line]));
    line++;
}

WorkFlow firstWorkFlow = workFlows.First(w => w.Name == "in");
for (int i = line + 1; i < inputLines.Length; i++)
{
    var part = new Part(inputLines[i]);
    WorkFlowProcessResult result = firstWorkFlow.ProcessWorkFlow(part);
    while (result.Result == WorkFlowProcessResultType.SentToNextWorkFlow)
    {
        WorkFlow nextWorkFlow = workFlows.First(wf => wf.Name == result.NextWorkFlowName);
        result = nextWorkFlow.ProcessWorkFlow(part);
    }

    if (result.Result == WorkFlowProcessResultType.Accepted)
        approvedParts.Add(part);
}

Console.WriteLine(approvedParts.Sum(p => p.GetSumOfRates()));
