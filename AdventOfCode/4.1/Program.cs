using _4._1;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");
string[] inputLines = File.ReadAllLines(inputFilePath);

var cardList = new List<Card>();
foreach (string line in inputLines)
    cardList.Add(new Card(line));

int sum = cardList.Sum(c => c.GetScore());
Console.WriteLine(sum);