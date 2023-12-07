
using _7._1;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);
var hands = new List<Hand>();
foreach (string line in inputLines)
{
    string[] lineParts = line.Split(' ');
    var hand = new Hand
    {
        Cards = lineParts[0].Select(card => new Card { Symbol = card }).ToArray(),
        Bet = int.Parse(lineParts[1])
    };

    hands.Add(hand);
}

hands = hands.Order(new HandComparer()).ToList();
int totalWinings = 0;
for (int i = 1; i <= hands.Count; i++)
{
    totalWinings += hands[i - 1].Bet * i;
}
Console.WriteLine(totalWinings);