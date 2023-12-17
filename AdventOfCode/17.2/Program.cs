using _17._2;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);
var map = new HeatLossMap(inputLines);

var result = map.DijkstraIt();

Console.WriteLine(result);