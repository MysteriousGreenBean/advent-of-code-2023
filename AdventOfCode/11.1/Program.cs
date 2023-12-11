using _11._1;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

List<string> galaxyInputs = File.ReadAllLines(inputFilePath).ToList();
string emptyRow = new string('.', galaxyInputs[0].Length);

int[] emptyRowNumbers = GetEmptyRowNumbers();
int[] emptyColumnNumbers = GetEmptyColumnNumbers();

DoubleEmptyColumns();
emptyRow = new string('.', galaxyInputs[0].Length);
DoubleEmptyRows();

List<Galaxy> galaxies = GatherGalaxies();

List<(Galaxy, Galaxy)> galaxyCombinations = new List<(Galaxy, Galaxy)>();
for (int i = 0; i < galaxies.Count; i++)
{
    for (int j = i + 1; j < galaxies.Count; j++)
    {
        galaxyCombinations.Add((galaxies[i], galaxies[j]));
    }
}

List<int> shortestDistances = new List<int>();
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

void DoubleEmptyColumns()
{
    foreach (int emptyColumnNumber in emptyColumnNumbers.Reverse())
    {
        for (int i = 0; i < galaxyInputs.Count; i++)
        {
            galaxyInputs[i] = galaxyInputs[i].Insert(emptyColumnNumber, ".");
        }
    }
}

void DoubleEmptyRows()
{
    foreach (int emptyRowNumber in emptyRowNumbers.Reverse())
    {
        galaxyInputs.Insert(emptyRowNumber, emptyRow);
    }
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

int CalculateShortestDistanceBetweenGalaxies(Galaxy galaxy1, Galaxy galaxy2)
    => Math.Abs(galaxy1.Row - galaxy2.Row) + Math.Abs(galaxy1.Column - galaxy2.Column); 

void PrintGalaxy()
{
    foreach (string galaxyInput in galaxyInputs)
    {
        Console.WriteLine(galaxyInput);
    }
}