using _12._1;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);

int matchingCombinationsSum = 0;
inputLines.AsParallel().ForAll(line =>
{
    Record rec = new Record(line);
    Interlocked.Add(ref matchingCombinationsSum, rec.GetMatchingCombinationsAmount());
});

Console.WriteLine(matchingCombinationsSum);