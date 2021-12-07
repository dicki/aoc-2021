    using System.Numerics;
    using System.Linq;

var inputLines = System.IO.File.ReadAllLines(@".\input.6.txt");
var hydrothermalVentures = new List<string>();
foreach (var line in inputLines)
{
    var hvLine = line.Split(" -> ");
    var hvLineStart = hvLine[0].Split(',');
    var hvLineEnd = hvLine[1].Split(',');
    var hvlineStartX = int.Parse(hvLineStart[0]);
    var hvlineStartY = int.Parse(hvLineStart[1]);
    var hvlineEndX = int.Parse(hvLineEnd[0]);
    var hvlineEndY = int.Parse(hvLineEnd[1]);

    // Diagonal lines
    if (hvlineStartX != hvlineEndX && hvlineStartY != hvlineEndY)
    {
        var minX = Math.Min(hvlineStartX, hvlineEndX);
        var maxX = Math.Max(hvlineStartX, hvlineEndX);
        var xRange = Enumerable.Range(minX, maxX - minX + 1).ToArray();
        if (hvlineStartX > hvlineEndX)
        {
            // Reverse order
            xRange = xRange.Reverse().ToArray();
        }

        var minY = Math.Min(hvlineStartY, hvlineEndY);
        var maxY = Math.Max(hvlineStartY, hvlineEndY);
        var yRange = Enumerable.Range(minY, maxY - minY + 1).ToArray();
        if (hvlineStartY > hvlineEndY)
        {
            // Reverse order
            yRange = yRange.Reverse().ToArray();
        }

        for (int i = 0; i < yRange.Count(); i++)
        {
            hydrothermalVentures.Add($"{xRange[i]},{yRange[i]}");
        }
    }
    else
    {
        if (hvlineStartX != hvlineEndX)
        {
            var xCoords = FindCoordinates(hvlineStartX, hvlineEndX);
            var xCoordsString = xCoords.Select(x => $"{x},{hvlineStartY}");
            hydrothermalVentures.AddRange(xCoordsString);
        }
        if (hvlineStartY != hvlineEndY)
        {
            var yCoords = FindCoordinates(hvlineStartY, hvlineEndY);
            var yCoordsString = yCoords.Select(y => $"{hvlineStartX},{y}");
            hydrothermalVentures.AddRange(yCoordsString);
        }
    }
}

System.Console.WriteLine("Count: " + hydrothermalVentures.Count());
var groupCount = hydrothermalVentures.GroupBy(hv => hv).Count(g => g.Count() > 1);
System.Console.WriteLine($"Count: {hydrothermalVentures.Count()} Group Count: {groupCount}");

int[] FindCoordinates(int start, int end)
{
    var min = Math.Min(start, end);
    var max = Math.Max(start, end);
    return Enumerable.Range(min, max - min + 1).ToArray();
}