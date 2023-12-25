using _25._1;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] lines = File.ReadAllLines(inputFilePath);
var components = new List<Component>();

var edges = new List<Edge>();
var karger = new KargerAlgorithm();
while (edges.Count != 3)
    edges = karger.FindMinimumCut(ReadComponents(lines));

Console.WriteLine(edges);

List<Component> ReadComponents(string[] lines)
{
    var components = new List<Component>();
    foreach (string line in lines)
    {
        string[] parts = line.Split(": ");
        string name = parts[0];
        string[] connectedComponents = parts[1].Split(' ');
        if (!components.Any(c => c.Name == name))
            components.Add(new Component(name));
        Component component = components.First(c => c.Name == name);
        foreach (string connectedComponentName in connectedComponents)
        {
            if (!components.Any(c => c.Name == connectedComponentName))
                components.Add(new Component(connectedComponentName));
            Component connectedComponent = components.First(c => c.Name == connectedComponentName);
            component.DirectlyConnectedComponents.Add(connectedComponent);
            connectedComponent.DirectlyConnectedComponents.Add(component);
        }
    }
    return components;
}