Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");
string[] inputLines = File.ReadAllLines(inputFilePath);
int sum = 0;
foreach (string line in inputLines)
{
    char firstDigit = line.First(c => char.IsDigit(c));
    char lastDigit = line.Last(c => char.IsDigit(c));
    sum += int.Parse($"{firstDigit}{lastDigit}");
}
Console.WriteLine(sum);
Console.ReadLine();