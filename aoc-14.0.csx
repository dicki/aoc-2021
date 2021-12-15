using System.Linq;

var input = File.ReadAllLines(@".\input.14.0.txt");

var foldCommands = input
    .Where(l => l.StartsWith("fold"))
    .ToArray();

var coordinates = new List<int[]>();
foreach (var line in input)
{
    if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("fold"))
    {
        coordinates.Add(line.Split(',')
            .Select(l => int.Parse(l))
            .ToArray());
    }
}

int yFoldCoord = int.Parse(foldCommands.First(fc => fc.StartsWith("fold along y="))["fold along y=".Length..]);
int xFoldCoord = int.Parse(foldCommands.First(fc => fc.StartsWith("fold along x="))["fold along x=".Length..]);

HashSet<string> markedCoords = new();
for (int i = 0; i < coordinates.Count(); i++)
{
    int[] coord = coordinates[i];
    int x = coord[0];
    // if (x >= xFoldCoord)
    // {
    //     x %= xFoldCoord;
    //     if (x != 0) x = xFoldCoord - x;
    // }
    int y = coord[1];
    if (y >= yFoldCoord)
    {
        y %= yFoldCoord;
        if (y != 0) y = yFoldCoord - y;
    }
    string coordKey = $"{x},{y}";
    // System.Console.WriteLine($"{coord[0]},{coord[1]} => "+ coordKey);
    if (!markedCoords.Contains(coordKey)) markedCoords.Add(coordKey);
}

System.Console.WriteLine($"No of marked coords: {markedCoords.Count()}");