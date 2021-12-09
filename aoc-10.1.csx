using System.Linq;

var input = System.IO.File.ReadAllLines(@".\input.10.1.txt").ToArray();
int inputLength = input.Length;
var lowPointList = new List<string>();
var coordinatesCounted = new HashSet<string>();
var basinList = new List<int>();
for (int yPos = 0; yPos < inputLength; yPos++)
{
    int lineLength = input[yPos].Length;
    for (int xPos = 0; xPos < lineLength; xPos++)
    {
        // Check Y coordinates
        var currPosHeight = input[yPos][xPos];
        if (yPos > 0)  // Check above current location
            if (input[yPos-1][xPos] <= currPosHeight) continue;
        if (yPos < inputLength - 1)  // Check below current location
            if (input[yPos+1][xPos] <= currPosHeight) continue;

        // Check X coordinates
        if (xPos > 0) // check to the left
            if (input[yPos][xPos-1] <= currPosHeight) continue;
        if (xPos < lineLength - 1) // check to the left
            if (input[yPos][xPos+1] <= currPosHeight) continue;

        lowPointList.Add($"{yPos},{xPos}");
    }
}

foreach (var point in lowPointList)
{
    var coordinates = point.Split(',').Select(p=>int.Parse(p)).ToArray();
    int basinSize = FindAndCalculateBasin(coordinates[0], coordinates[1], input);
    basinList.Add(basinSize);
}

System.Console.WriteLine(basinList.OrderByDescending(p => p).Take(3).Aggregate((p1,p2)=> p1 * p2 ));

int FindAndCalculateBasin(int yPos, int xPos, string[] input)
{
    int basinSize = 0;

    int lineLength = input[yPos].Length;
    var currPosHeight = input[yPos][xPos];
    var coordKey = $"{yPos},{xPos}";

    if (coordinatesCounted.Contains(coordKey)) return 0;
    coordinatesCounted.Add(coordKey);
    if (currPosHeight == '9') return 0;


    if (yPos > 0)  // Check above current location
        if (input[yPos-1][xPos] > currPosHeight) basinSize += FindAndCalculateBasin(yPos-1,xPos,input);
    if (yPos < inputLength - 1)  // Check below current location
        if (input[yPos+1][xPos] > currPosHeight) basinSize += FindAndCalculateBasin(yPos+1,xPos,input);

    // Check X coordinates
    if (xPos > 0) // check to the left
        if (input[yPos][xPos-1] > currPosHeight) basinSize += FindAndCalculateBasin(yPos,xPos-1,input);
    if (xPos < lineLength - 1) // check to the left
        if (input[yPos][xPos+1] > currPosHeight) basinSize += FindAndCalculateBasin(yPos,xPos+1,input);
    
    return 1 + basinSize;
}