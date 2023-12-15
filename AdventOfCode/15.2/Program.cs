using _15._2;
using System.Text;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

//read input file by chars using streams
using FileStream inputStream = File.OpenRead(inputFilePath);
using StreamReader streamReader = new StreamReader(inputStream);

StringBuilder currentInstruction = new StringBuilder();
var boxes = new Box[256];
for (int i = 0; i < 256; i++)
    boxes[i] = new Box() { Id = i };
var steps = new List<Step>();
while (!streamReader.EndOfStream)
{
    char currentchar = (char)streamReader.Read();
    if (currentchar == (int)'\n')
        continue;
    if (currentchar == ',')
    {
        steps.Add(new Step(currentInstruction.ToString()));
        currentInstruction.Clear();
        continue;
    }
    currentInstruction.Append(currentchar);
}
steps.Add(new Step(currentInstruction.ToString()));

foreach (Step step in steps)
    step.Execute(boxes);

long sum = 0;
foreach (Box box in boxes)
    sum += box.GetFocusingPower();

Console.WriteLine(
    $"Sum is {sum}");
