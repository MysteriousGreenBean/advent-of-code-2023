using _10._1;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);

var pipeMap = new PipeMap(inputLines);

var previousPipeField = pipeMap.StartingPipeField;
var currentPipeField = pipeMap.MoveByStartingPipe();
int stepsDone = 1;
while (currentPipeField != pipeMap.StartingPipeField)
{
    PipeField nextPipeField = pipeMap.MoveByPipe(currentPipeField, previousPipeField);
    previousPipeField = currentPipeField;
    currentPipeField = nextPipeField;
    stepsDone++;
}

Console.WriteLine($"Total steps in the loop: {stepsDone}");
Console.WriteLine($"Farthest distance from start: {stepsDone/2}");