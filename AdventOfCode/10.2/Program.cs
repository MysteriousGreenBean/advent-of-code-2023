using _10._2;
using System.Text.RegularExpressions;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);

var pipeMap = new PipeMap(inputLines);
var pipeFields = new List<PipeField>();

var previousPipeField = pipeMap.StartingPipeField;
pipeFields.Add(previousPipeField);
var currentPipeField = pipeMap.MoveByStartingPipe();
pipeFields.Add(currentPipeField);
int stepsDone = 1;
while (currentPipeField != pipeMap.StartingPipeField)
{
    PipeField nextPipeField = pipeMap.MoveByPipe(currentPipeField, previousPipeField);
    pipeFields.Add(nextPipeField);
    previousPipeField = currentPipeField;
    currentPipeField = nextPipeField;
    stepsDone++;
}

pipeFields = pipeFields.Distinct().ToList();

var FJL7Regex = new Regex(@"(F-*J)|(L-*7)|(F-*7)|(L-*J)|(\|)");
int groundInsideCount = 0;
int allGroundCount = 0;


for (int row = 0; row < pipeMap.PipeFields.GetLength(0); row++)
{
    for (int column = 0; column < pipeMap.PipeFields.GetLength(1); column++)
    {
        if (pipeMap.PipeFields[row, column].Symbol == '.')
        {
            if (!IsEnclosedByPipe(pipeFields, column, row))
                groundInsideCount++;

        }
    }
}

Console.WriteLine($"Count: {groundInsideCount}");
Console.WriteLine($"All ground count: {allGroundCount}");
Console.WriteLine($"Steps: {stepsDone}");
Console.WriteLine($"Fields of pipe: {pipeFields.Count}");

bool IsEnclosedByPipe(List<PipeField> pipe, int column, int row)
{
    int count = pipe.Count;
    bool inside = false;

    for (int i = 0, j = count - 1; i < count; j = i++)
    {
        if ((pipe[i].Row > row != pipe[j].Row > row) &&
            (column < (pipe[j].Column - pipe[i].Column) * (row - pipe[i].Row) / (pipe[j].Row - pipe[i].Row) + pipe[i].Column))
        {
            inside = !inside;
        }
    }

    return inside;
}