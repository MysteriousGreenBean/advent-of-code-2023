using _18._1;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);
var field = new Field();

foreach (var instruction in inputLines.Select(line => new DigInstruction(line)).ToArray())
{
    field.PerformDigInstruction(instruction);
}

string directory = Path.GetDirectoryName(inputFilePath);
field.Print(Path.Combine(directory, "outline.txt"));
field.DigInside();
field.Print(Path.Combine(directory, "fullMap.txt"));
Console.WriteLine($"Cubic meters: {field.CalculateCubicMeters()}");