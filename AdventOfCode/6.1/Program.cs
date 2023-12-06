using _6._1;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

// For once I'm making my input easier
// I assume input consists of 2 numbers per line, separated by a space
// first number is time, second is distance

string[] inputLines = File.ReadAllLines(inputFilePath);
int result = 1;
foreach (string[] lineNumbers in inputLines.Select(il => il.Split(' ')))
{
    var raceStats = new RaceStats
    {
        Time = int.Parse(lineNumbers[0]),
        Distance = int.Parse(lineNumbers[1])
    };
    (int rangeStart, int rangeEnd) = raceStats.GetRangeOfValidButtonHoldingTimes();
    int amountOfValidButtonHoldingTimes = rangeEnd - rangeStart + 1;
    Console.WriteLine($"Number of ways to win a race: {amountOfValidButtonHoldingTimes}");
    result *= amountOfValidButtonHoldingTimes;
}
Console.WriteLine(result);