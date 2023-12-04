using _4._2;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");
string[] inputLines = File.ReadAllLines(inputFilePath);

var cardList = new List<Card>();
foreach (string line in inputLines)
    cardList.Add(new Card(line));

for (int i = 0; i < cardList.Count; i++)
{
    Card card = cardList[i];
    int wonCardCopiesAmount = card.GetWonAmount();
    for (int c = 0; c < card.CopiesAmount; c++)
    for (int j = i + 1; j < i + wonCardCopiesAmount + 1; j++)
        cardList[j].CopiesAmount++;
}

int totalCardAmount = cardList.Sum(card => card.CopiesAmount);
Console.WriteLine(totalCardAmount);
