using _9._1;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);

var sequences = inputLines.Select(l => new Sequence(l.Split(' ').Select(n => int.Parse(n)).ToArray())).ToArray();

int sumOfExtrapolatedNumbers = 0;
foreach (var sequence in sequences)
    sumOfExtrapolatedNumbers += sequence.ExtrapolateNextNumber();

Console.WriteLine(sumOfExtrapolatedNumbers);