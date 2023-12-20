using _20._1;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);

var modules = new ModuleList(inputLines);

var button = new ButtonModule(modules);

for (int i = 0; i < 1000; i++)
    button.Push();

Console.WriteLine($"High pulses: {button.HighPulsesSent}\r\nLow pulses: {button.LowPulsesSent}\r\nMultiplied together: {button.HighPulsesSent * button.LowPulsesSent}");