using _5._1;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");
string[] inputLines = File.ReadAllLines(inputFilePath);

long[] seedNumbers = inputLines[0].Replace("seeds: ", string.Empty)
    .Split(' ')
    .Select(long.Parse)
    .ToArray();

int lineIndex = 1;

var seedToSoil = new Map(GetLinesForMap());
var soilToFertilizer = new Map(GetLinesForMap());
var fertilizerToWater = new Map(GetLinesForMap());
var waterToLight = new Map(GetLinesForMap());
var lightToTemperature = new Map(GetLinesForMap());
var temperatureToHumidity = new Map(GetLinesForMap());
var humidityToLocation = new Map(GetLinesForMap());

long[] locations = seedNumbers.Select(GetLocation).ToArray();
Console.WriteLine(locations.Min());

long GetLocation(long seedNumber)
{
    long soil = seedToSoil!.GetMappedValue(seedNumber);
    long fertilizer = soilToFertilizer!.GetMappedValue(soil);
    long water = fertilizerToWater!.GetMappedValue(fertilizer);
    long light = waterToLight!.GetMappedValue(water);
    long temperature = lightToTemperature!.GetMappedValue(light);
    long humidity = temperatureToHumidity!.GetMappedValue(temperature);
    long location = humidityToLocation!.GetMappedValue(humidity);
    return location;
}

IEnumerable<string> GetLinesForMap()
{
    lineIndex += 2;
    var linesForMap = new List<string>();
    do
    {
        string line = inputLines[lineIndex++];
        linesForMap.Add(line);
    }
    while (lineIndex < inputLines.Length && !string.IsNullOrWhiteSpace(inputLines[lineIndex]));

    return linesForMap;
}

