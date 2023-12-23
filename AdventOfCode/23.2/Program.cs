using _23._2;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = @"C:\Users\pako\Downloads\input.txt";
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);
var map = new Map(inputLines);

Console.WriteLine(map.FindLongestPath());