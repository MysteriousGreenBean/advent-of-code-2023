using _22._2;

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

long totalFellDownAmount = 0;
var bricksFellCache = new Dictionary<Brick, HashSet<Brick>>();
int processedBricks = 0;
foreach (var brick in bricks.OrderByDescending(b => Math.Max(b.Start.Z, b.End.Z)))
{
    var arrayCopy = bricks.Except(new[] { brick }).ToArray();
    var fellBricks = new HashSet<Brick>(new BrickComparer());

    var readFromCache = new HashSet<Brick>(new BrickComparer());
    foreach (var otherBrick in arrayCopy)
    {
        if (!otherBrick.IsAnythingBeneathMe(arrayCopy) && bricksFellCache.ContainsKey(otherBrick))
        {
            fellBricks.Add(otherBrick);
            readFromCache.Add(otherBrick);
            fellBricks.UnionWith(bricksFellCache[otherBrick]);
        }
    }

    var nonCachedBricks = arrayCopy.Except(readFromCache).ToArray();

    while (nonCachedBricks.Any(b => !b.IsAnythingBeneathMe(nonCachedBricks)))
    {
        foreach (var otherBrick in nonCachedBricks)
        {
            if (!otherBrick.IsAnythingBeneathMe(nonCachedBricks))
            {
                otherBrick.MoveDown();
                fellBricks.Add(otherBrick);
            }
        }
    }

    processedBricks++;
    if (!bricksFellCache.ContainsKey(brick))
        bricksFellCache.Add(brick, fellBricks);
    else
        bricksFellCache[brick].UnionWith(fellBricks);

    totalFellDownAmount += fellBricks.Count;
}

Console.WriteLine($"Sum of fell down: {totalFellDownAmount}");