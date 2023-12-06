using _6._2;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

// For once I'm making my input easier
// I assume input consists of 2 numbers total
// First line is Time
// Second line is distance

string[] inputLines = File.ReadAllLines(inputFilePath);
var raceStats = new RaceStats
{
    Time = long.Parse(inputLines[0]),
    Distance = long.Parse(inputLines[1])
};

(long rangeStart, long rangeEnd) = raceStats.GetRangeOfValidButtonHoldingTimes();
long amountOfValidButtonHoldingTimes = rangeEnd - rangeStart + 1;
Console.WriteLine($"Number of ways to win a race: {amountOfValidButtonHoldingTimes}");