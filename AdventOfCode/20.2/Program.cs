using _20._2;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);

var modules = new ModuleList(inputLines);

var button = new ButtonModule(modules);

while (!button.Push()) ;

long minimumButtonPushes = 1;
foreach (long mfInputHigh in button.MfInputHighs)
    minimumButtonPushes = FindLCM(minimumButtonPushes, mfInputHigh);

Console.WriteLine($"Button pushes required: {minimumButtonPushes} ");

long FindGCD(long a, long b)
{
    while (b != 0)
    {
        long temp = b;
        b = a % b;
        a = temp;
    }
    return a;
}

long FindLCM(long a, long b)
{
    return (a * b) / FindGCD(a, b);
}