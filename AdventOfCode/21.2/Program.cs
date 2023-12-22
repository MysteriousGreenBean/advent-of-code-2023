using _21._2;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);
var map = new Map(inputLines);

Console.WriteLine($"Finding point to find F(x)=ax^2+bx+c");
int a = map.PossibleTargets(65);
Console.WriteLine($"a: {a}"); 
map = new Map(inputLines);
int b = map.PossibleTargets(196);
Console.WriteLine($"b: {b}");
int c = map.PossibleTargets(327);
Console.WriteLine($"c: {c}");

Console.WriteLine($"F(x)={a}x^2+{b}x+{c}");
Console.WriteLine($"F(26501365)={CalculateF(26501365)}");

long CalculateF(long x)
    => a * x * x + b * x + c;