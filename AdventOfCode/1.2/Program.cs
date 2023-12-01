Dictionary<string, char> dict = new Dictionary<string, char>()
{
    { "one", '1' },
    { "two", '2' },
    { "three", '3' },
    { "four", '4' },
    { "five", '5' },
    { "six", '6' },
    { "seven", '7' },
    { "eight", '8' },
    { "nine", '9' }
};

string[] wordDigit = dict.Keys.ToArray();

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");
string[] inputLines = File.ReadAllLines(inputFilePath);
int sum = 0;

foreach (string line in inputLines)
{
    char firstDigit = GetFirstDigit(line);
    char lastDigit = GetLastDigit(line);
    sum += int.Parse($"{firstDigit}{lastDigit}");
}
Console.WriteLine(sum);
Console.ReadLine();

char GetFirstDigit(string line)
{
    int smallestIndex = int.MaxValue;
    char smallestIndexDigit = '0';
    foreach (string word in wordDigit)
        if (line.Contains(word))
        {
            int indexOfWord = line.IndexOf(word);
            smallestIndex = Math.Min(smallestIndex, indexOfWord);
            if (smallestIndex == indexOfWord)
                smallestIndexDigit = dict[word];
        }

    char firstDigit = line.FirstOrDefault(c => char.IsDigit(c));
    int indexOfFirstDigit = line.IndexOf(firstDigit);
    smallestIndex = Math.Min(smallestIndex, indexOfFirstDigit);

    if (indexOfFirstDigit < 0 || smallestIndex != indexOfFirstDigit)
        return smallestIndexDigit;
    else
        return firstDigit;
}

char GetLastDigit(string line)
{
    int biggestIndex = int.MinValue;
    char biggestIndexDigit = '0';
    foreach (string word in wordDigit)
        if (line.Contains(word))
        {
            int indexOfWord = line.LastIndexOf(word);
            biggestIndex = Math.Max(biggestIndex, indexOfWord);
            if (biggestIndex == indexOfWord)
                biggestIndexDigit = dict[word];
        }

    char lastdigit = line.LastOrDefault(c => char.IsDigit(c));
    int indexOfLastDigit = line.LastIndexOf(lastdigit);
    biggestIndex = Math.Max(biggestIndex, indexOfLastDigit);

    if (indexOfLastDigit < 0 || biggestIndex != indexOfLastDigit)
        return biggestIndexDigit;
    else
        return lastdigit;
}