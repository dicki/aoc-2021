using System.Linq;

var input = File.ReadAllLines(@".\input.14.1.txt");

var foldCommands = input
    .Where(l => l.StartsWith("fold"))
    .Select(l => l["fold along ".Length..])
    .ToArray();

// var coordinates = input
//     .Where(l => !string.IsNullOrWhiteSpace(l) && !l.StartsWith("fold"))
//     .Select(line => line
//                     .Split(',')
//                     .Select(l => int.Parse(l))
//                     .ToArray())
//     .ToArray();

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

HashSet<string> markedCoords = new();
foreach (var foldCommand in foldCommands)
{
    System.Console.WriteLine(foldCommand);
    bool yfold = false, xfold = false;
    int xFoldCoord = 0, yFoldCoord = 0;
    if (foldCommand.Contains("y"))
    {
        yfold = true;
        yFoldCoord = int.Parse(foldCommand["y=".Length..]);
    }
    else
    {
        xfold = true;
        xFoldCoord = int.Parse(foldCommand["x=".Length..]);
    }
    var newCoordinates = new List<int[]>();
    for (int i = 0; i < coordinates.Count(); i++)
    {
        int[] coord = coordinates[i];
        int x = coord[0];
        if (xfold && x >= xFoldCoord)
        {
            x %= xFoldCoord;
            if (x != 0) x = xFoldCoord - x;
        }
        int y = coord[1];
        if (yfold && y >= yFoldCoord)
        {
            y %= yFoldCoord;
            if (y != 0) y = yFoldCoord - y;
        }
        string coordKey = $"{x},{y}";
        // System.Console.WriteLine($"{coord[0]},{coord[1]} => "+ coordKey);
        if (!markedCoords.Contains(coordKey))
        {
            markedCoords.Add(coordKey);
            newCoordinates.Add(new int[]{ x, y });
        }
    }
    int maxX = newCoordinates.Max(nc => nc[0]);
    int maxY = newCoordinates.Max(nc => nc[1]);
    for (int i = 0; i <= maxX; i++)
    {
        for (int j = 0; j <= maxY; j++)
        {
            string coordKey = $"{i},{j}";
            System.Console.Write(markedCoords.Contains(coordKey) ? "#" : ".");
        }
        System.Console.WriteLine();
    }
    System.Console.WriteLine();
    System.Console.WriteLine();
    markedCoords.Clear();

    coordinates = newCoordinates;
}


System.Console.WriteLine($"No of marked coords: {markedCoords.Count()}");