using _12._2;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);

long matchingCombinationsSum = 0;
foreach (string inputLine in inputLines)
{
    matchingCombinationsSum += new Record(inputLine).GetMatchingCombinationsAmount();
}

Console.WriteLine(matchingCombinationsSum);