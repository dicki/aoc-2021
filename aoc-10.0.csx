using System.Linq;

var input = System.IO.File.ReadAllLines(@".\input.10.1.txt").ToArray();
int inputLength = input.Length;
var lowPointList = new List<int>();
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

        // add point to list
        // System.Console.WriteLine("lowpoint: " + currPosHeight);
        lowPointList.Add((currPosHeight & 15)+1);
    }
}

System.Console.WriteLine(lowPointList.Sum());
