using _14._2;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = @"C:\Users\Patryk\Downloads\input.txt";
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);
Panel panel = new Panel(inputLines);
int cycles = 1000000000;

panel.FindCycle();
panel.MoveByCycles(cycles);

Console.WriteLine($"Load: {panel.CalculateLoad()}");
