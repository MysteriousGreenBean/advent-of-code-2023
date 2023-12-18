using _18._2;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);

var field = new Field();
field.PerformDigInstructions(inputLines.Select(line => new DigInstruction(line)));

Console.WriteLine(field.CalculateLagoonArea());