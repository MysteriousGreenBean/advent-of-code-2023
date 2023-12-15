Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

//read input file by chars using streams
using FileStream inputStream = File.OpenRead(inputFilePath);
using StreamReader streamReader = new StreamReader(inputStream);

long sum = 0;
int currentValue = 0;
while (!streamReader.EndOfStream)
{
    int charValue = streamReader.Read();
    if (charValue == (int)'\n')
        continue;
    if (charValue == ',')
    {
        sum += currentValue;
        currentValue = 0;
        continue;
    }
    currentValue += charValue;
    currentValue *= 17;
    currentValue = currentValue % 256;
}
sum += currentValue;

Console.WriteLine(
    $"Sum is {sum}");