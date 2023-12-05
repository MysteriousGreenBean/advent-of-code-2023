using _5._2;
using System.Collections.Concurrent;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");
string[] inputLines = File.ReadAllLines(inputFilePath);

int lineIndex = 1;

var seedToSoil = new Map(GetLinesForMap());
var soilToFertilizer = new Map(GetLinesForMap());
var fertilizerToWater = new Map(GetLinesForMap());
var waterToLight = new Map(GetLinesForMap());
var lightToTemperature = new Map(GetLinesForMap());
var temperatureToHumidity = new Map(GetLinesForMap());
var humidityToLocation = new Map(GetLinesForMap());

IEnumerable<long> firstLineNumbers = inputLines[0].Replace("seeds: ", string.Empty)
    .Split(' ')
    .Select(long.Parse);

var seedRanges = firstLineNumbers.Select((value, index) => (value, index))
       .GroupBy(tuple => tuple.index / 2, tuple => tuple.value)
       .Select(group => (start: group.First(), length: group.Last()));

var locations = new ConcurrentBag<long>();
seedRanges.AsParallel().ForAll(seedRange => GetSeedNumbers(seedRange.start, seedRange.length)
    .AsParallel()
       .ForAll(seedNumber => locations.Add(GetLocation(seedNumber))));


Console.WriteLine(locations.Min());

IEnumerable<long> GetSeedNumbers(long start, long length)
{
    for (long seedNumber = start; seedNumber < start + length; seedNumber++)
        yield return seedNumber;
}


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

