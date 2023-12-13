using _13._1;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);
var patternLines = new List<string>();
var patterns = new List<Pattern>();

foreach (string line in inputLines)
{
    if (string.IsNullOrEmpty(line))
    {
        patterns.Add(new Pattern(patternLines.ToArray()));
        patternLines.Clear();
    }
    else
        patternLines.Add(line);
}
patterns.Add(new Pattern(patternLines.ToArray()));


long calculatedSum = 0;
foreach (Pattern pattern in patterns)
{
    int row = pattern.GetHorizontalMirrorIndex();
    if (row != -1)
        calculatedSum += 100 * (row + 1);
    else
    {
        int column = pattern.GetVerticalMirrorIndex();
        if (column != -1)
            calculatedSum += column + 1;
    }
}

Console.WriteLine(calculatedSum);