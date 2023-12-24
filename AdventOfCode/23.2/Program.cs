using _23._2;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);


var thread = new Thread(LongestPath, 8 * 1024 * 1024);
thread.Start(inputLines);
thread.Join();

void LongestPath(object? parameter)
{
    if (parameter == null)
        throw new ArgumentNullException(nameof(parameter));

    var map = new Map((string[])parameter);
    Console.WriteLine(map.FindLongestPath());
}