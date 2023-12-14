using _14._1;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);
Panel panel = new Panel(inputLines);
panel.Print();
panel.TiltNorth();
panel.Print();
Console.WriteLine($"Load: {panel.CalculateLoad()}");