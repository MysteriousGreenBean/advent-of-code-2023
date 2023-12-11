using _11._2;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = @"C:\Users\Patryk\Downloads\input.txt";
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

List<string> galaxyInputs = File.ReadAllLines(inputFilePath).ToList();
int galaxyExpandRate = 1000000;

string emptyRow = new string('.', galaxyInputs[0].Length);

int[] emptyRowNumbers = GetEmptyRowNumbers();
int[] emptyColumnNumbers = GetEmptyColumnNumbers();


List<Galaxy> galaxies = GatherGalaxies();

List<(Galaxy, Galaxy)> galaxyCombinations = new List<(Galaxy, Galaxy)>();
for (int i = 0; i < galaxies.Count; i++)
{
    for (int j = i + 1; j < galaxies.Count; j++)
    {
        galaxyCombinations.Add((galaxies[i], galaxies[j]));
    }
}

var shortestDistances = new List<long>();
foreach ((Galaxy, Galaxy) galaxyCombination in galaxyCombinations)
{
    string tag = $"{galaxyCombination.Item1.Id}-{galaxyCombination.Item2.Id}";
    shortestDistances.Add(CalculateShortestDistanceBetweenGalaxies(galaxyCombination.Item1, galaxyCombination.Item2));
}


Console.WriteLine(shortestDistances.Sum());

int[] GetEmptyRowNumbers()
{
    var emptyRowNumbers = new List<int>();
    for (int i = 0; i < galaxyInputs.Count; i++)
    {
        if (galaxyInputs[i] == emptyRow)
            emptyRowNumbers.Add(i);
    }

    return emptyRowNumbers.ToArray();
}

int[] GetEmptyColumnNumbers()
{
    var emptyColumnNumbers = new List<int>();
    for (int i = 0; i < galaxyInputs[0].Length; i++)
    {
        bool emptyColumn = true;
        for (int j = 0; j < galaxyInputs.Count; j++)
        {
            if (galaxyInputs[j][i] != '.')
            {
                emptyColumn = false;
                break;
            }
        }

        if (emptyColumn)
            emptyColumnNumbers.Add(i);
    }

    return emptyColumnNumbers.ToArray();
}

List<Galaxy> GatherGalaxies()
{
    int id = 1;
    var galaxies = new List<Galaxy>();
    for (int i = 0; i < galaxyInputs.Count; i++)
    {
        for (int j = 0; j < galaxyInputs[i].Length; j++)
        {
            char field = galaxyInputs[i][j];
            if (field != '.')
                galaxies.Add(new Galaxy
                {
                    Id = id++,
                    Row = i,
                    Column = j
                });
        }
    }
    return galaxies;
}

long CalculateShortestDistanceBetweenGalaxies(Galaxy galaxy1, Galaxy galaxy2)
{
    int smallerRow = galaxy1.Row <= galaxy2.Row ? galaxy1.Row : galaxy2.Row;
    int biggerRow = galaxy1.Row >= galaxy2.Row ? galaxy1.Row : galaxy2.Row;
    int smallerColumn = galaxy1.Column <= galaxy2.Column ? galaxy1.Column : galaxy2.Column;
    int biggerColumn = galaxy1.Column >= galaxy2.Column ? galaxy1.Column : galaxy2.Column;

    int originalRowDistance = biggerRow - smallerRow;
    int originalColumnDistance = biggerColumn - smallerColumn;

    //count all empty rows between galaxy 1 and galaxy 2
    int emptyRowsBetweenGalaxies = 0;
    for (int i = smallerRow + 1; i < biggerRow; i++)
    {
        if (emptyRowNumbers.Contains(i))
            emptyRowsBetweenGalaxies++;
    }
    // count all empty columns between galaxy 1 and galaxy 2
    int emptyColumnsBetweenGalaxies = 0;
    for (int i = smallerColumn + 1; i < biggerColumn; i++)
    {
        if (emptyColumnNumbers.Contains(i))
            emptyColumnsBetweenGalaxies++;
    }

    long rowDistance = originalRowDistance + emptyRowsBetweenGalaxies * (galaxyExpandRate - 1);
    long columnDistance = originalColumnDistance + emptyColumnsBetweenGalaxies * (galaxyExpandRate - 1);
    return rowDistance + columnDistance;
}
