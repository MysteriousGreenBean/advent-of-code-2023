using _22._1;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);
var bricks = inputLines.Select(line =>
{
    string[] splitLines = line.Split('~');
    string[] startCoords = splitLines[0].Split(',');
    string[] endCoords = splitLines[1].Split(',');
    return new Brick(new Coords(int.Parse(startCoords[0]), int.Parse(startCoords[1]), int.Parse(startCoords[2])),
               new Coords(int.Parse(endCoords[0]), int.Parse(endCoords[1]), int.Parse(endCoords[2])));
}).ToArray();


while (bricks.Any(b => !b.IsAnythingBeneathMe(bricks)))
{
    foreach (var brick in bricks)
    {
        if (!brick.IsAnythingBeneathMe(bricks))
            brick.MoveDown();
    }
}

int canBeDisintegrated = 0;
foreach (var brick in bricks)
{
    var arrayCopy = bricks.Except(new[] { brick }).ToArray();
    bool areAllSafe = true;
    foreach (var brick2 in arrayCopy)
    {
        if (!brick2.IsAnythingBeneathMe(arrayCopy))
            areAllSafe = false;
    }
    if (areAllSafe)
        canBeDisintegrated++;
}

Console.WriteLine($"Can be disintegrated: {canBeDisintegrated}");