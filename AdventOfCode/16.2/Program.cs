using _16._2;

Console.WriteLine("Provide path to input file:");
string? inputFilePath = "C:\\Users\\Patryk\\Downloads\\input.txt";
if (!File.Exists(inputFilePath))
    throw new InvalidDataException("Provided file does not exist");

string[] inputLines = File.ReadAllLines(inputFilePath);

var originalFieldMap = new FieldMap(inputLines);

long maxEnerizedFields = 0;

// test first row
for (int i = 0; i < originalFieldMap.Columns; i++)
{
    var copyFieldMap = originalFieldMap.Copy();
    copyFieldMap.SimulateBeam(new Beam(0, i, BeamDirection.Down));
    maxEnerizedFields = Math.Max(maxEnerizedFields, copyFieldMap.GetEnergizedFieldsCount());
}

// test first column
for (int i = 0; i < originalFieldMap.Rows; i++)
{
    var copyFieldMap = originalFieldMap.Copy();
    copyFieldMap.SimulateBeam(new Beam(i, 0, BeamDirection.Right));
    maxEnerizedFields = Math.Max(maxEnerizedFields, copyFieldMap.GetEnergizedFieldsCount());
}

// test last row
for (int i = 0; i < originalFieldMap.Columns; i++)
{
    var copyFieldMap = originalFieldMap.Copy();
    copyFieldMap.SimulateBeam(new Beam(originalFieldMap.Rows - 1, i, BeamDirection.Up));
    maxEnerizedFields = Math.Max(maxEnerizedFields, copyFieldMap.GetEnergizedFieldsCount());
}

// test last column
for (int i = 0; i < originalFieldMap.Rows; i++)
{
    var copyFieldMap = originalFieldMap.Copy();
    copyFieldMap.SimulateBeam(new Beam(i, originalFieldMap.Columns - 1, BeamDirection.Left));
    maxEnerizedFields = Math.Max(maxEnerizedFields, copyFieldMap.GetEnergizedFieldsCount());
}


originalFieldMap.SimulateBeam(new Beam(0, 0, BeamDirection.Right));

Console.WriteLine($"Energized fields: {maxEnerizedFields}");
