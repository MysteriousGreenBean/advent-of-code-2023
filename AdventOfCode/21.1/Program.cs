using _21._1;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = "C:\\Users\\Patryk\\Downloads\\input.txt";
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);
var map = new Map(inputLines);
Console.WriteLine($"Possible targets: {map.PossibleTargets(64)}");
int steps = 1;