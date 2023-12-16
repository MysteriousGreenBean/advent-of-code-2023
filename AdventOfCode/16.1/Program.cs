using _16._1;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = Console.ReadLine();
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);

int rows = inputLines.Length;
int columns = inputLines[0].Length;

var fields = new Field[rows, columns];
for (int row = 0; row < rows; row++)
{
    for (int column = 0; column < columns; column++)
    {
        fields[row, column] = new Field(row, column, inputLines[row][column]);
    }
}

var beams = new List<Beam>() { new Beam(0, 0, BeamDirection.Right) };
while (beams.Count > 0)
{
    var beamsToAdd = new List<Beam>();
    foreach (Beam beam in beams)
    {
        Field field = fields[beam.Row, beam.Column];
        BeamMoveResult[] results = field.MoveThroughField(beam);
        if (results.Length > 0)
        {
            beam.Move(results[0]);
            for (int i = 1; i < results.Length; i++)
                beamsToAdd.Add(new Beam(results[i].NewRow, results[i].NewColumn, results[i].NewDirection));
        }
    }
    beams.AddRange(beamsToAdd);
    beamsToAdd.Clear();
    beams.RemoveAll(IsBeamOutOfBounds);
    beams.RemoveAll(beam => beam.IsLooped);
}

int energizedFields = 0;
foreach (Field field in fields)
    if (field.IsEnergized)
        energizedFields++;

Console.WriteLine($"Energized fields: {energizedFields} Beam count: {beams.Count}");

bool IsBeamOutOfBounds(Beam beam) => beam.Row < 0 || beam.Row >= rows || beam.Column < 0 || beam.Column >= columns;