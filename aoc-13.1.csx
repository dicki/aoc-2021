using System.Linq;

var caveConnections = File.ReadAllLines(@".\input.13.0.txt")
    .Select(line => line.Split('-')).ToArray();

Dictionary<string, List<string>> caveConnectionMap = new();

foreach (var caveConnection in caveConnections)
{
    caveConnectionMap.AddOrUpdate(caveConnection[0], caveConnection[1]);
    caveConnectionMap.AddOrUpdate(caveConnection[1], caveConnection[0]);
}

var smallCaves = caveConnectionMap.Keys.Where(k => (k != "start" || k != "end") && char.IsLower(k[0]));
var validPaths = new List<string>();

foreach (var caveConnection in caveConnections.Where(c => c.Any(cc => cc == "start")))
{
    string startPoint, destPoint;
    if (caveConnection[0] == "start")
    {
         startPoint = caveConnection[0];
         destPoint = caveConnection[1];
    }
    else
    {
         startPoint = caveConnection[1];
         destPoint = caveConnection[0];
    }

    foreach (var smallCave in smallCaves)
    {
        MapCavePathway(startPoint, destPoint, smallCave);
    }
}

validPaths = validPaths.Distinct().OrderBy(v => v).ToList();

// foreach(var validPath in validPaths)
// {
//     System.Console.WriteLine(validPath);
// }

System.Console.WriteLine($"No of valid paths: {validPaths.Count()}");

void MapCavePathway(string path, string destPoint, string smallCaveMultiVisits)
{
    var newPathPoint = $",{destPoint}";
    var newPath = path + newPathPoint;
    if (destPoint == "end") 
    {
        validPaths.Add(newPath);
        return;
    }
    // rules
    bool isSmallCave = char.IsLower(destPoint[0]);
    if (isSmallCave) // lower case point can only be visited once, except the one on smallCaveMultiVisits parameter it can be visited twice
    {
        if (smallCaveMultiVisits == destPoint)
        {
            if (path.Split(',').Count(p => p == destPoint) > 1) return; // stop
        }
        else if (path.Contains(newPathPoint)) return;
    }
    if (destPoint == "start") // start points can only be visited once
    {
        return; // stop
    }
    if (caveConnectionMap.ContainsKey(destPoint))
    {
        foreach (var point in caveConnectionMap[destPoint])
        {
            MapCavePathway(newPath, point, smallCaveMultiVisits);
        }
    }
}

static void AddOrUpdate(this Dictionary<string, List<string>> targetDictionary, string key, string entry)
{
    if (!targetDictionary.ContainsKey(key))
        targetDictionary.Add(key, new List<string>());

    targetDictionary[key].Add(entry);
}