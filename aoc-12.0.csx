using System.Linq;

var input = File.ReadAllLines(@".\input.12.0.txt")
    .Select(line => line.Select(c => c & 15).ToArray())
    .ToArray();

int noOfSteps = 100;
HashSet<string> flashedDuringStep = new HashSet<string>();

int noOfFlashes = 0;

for (int step = 0; step < noOfSteps; step++)
{
    for (int yPos = 0; yPos < input.Length; yPos++)
    {
        for (int xPos = 0; xPos < input[yPos].Length; xPos++)
        {
            input[yPos][xPos] += 1;
            if (input[yPos][xPos] > 9)
            {
                // Flash
                FlashOctopus(yPos, xPos);
            }
        }
        // System.Console.WriteLine();
    }

    foreach (var flashedOcto in flashedDuringStep)
    {
        var yPos = flashedOcto[0] & 15;
        var xPos = flashedOcto[2] & 15;
        input[yPos][xPos] = 0;
    }
    flashedDuringStep.Clear();
}

System.Console.WriteLine($"No of flashes: {noOfFlashes}");

void FlashOctopus(int yPos, int xPos)
{
    string octopusKey = $"{yPos},{xPos}";
    if (flashedDuringStep.Contains(octopusKey)) return;
    flashedDuringStep.Add(octopusKey);

    int maxYPos = input.Length - 1;
    int maxXPos = input[yPos].Length - 1;

    bool checkLeft = xPos - 1 >= 0;
    bool checkAbove = yPos - 1 >= 0;
    bool checkRight = xPos + 1 <= maxXPos;
    bool checkBelow = yPos + 1 <= maxYPos;

    noOfFlashes++;

    input[yPos][xPos] = 0;
    
    if (checkLeft)
    {
        // check to the LEFT
        input[yPos][xPos - 1] += 1;
        if (input[yPos][xPos - 1] > 9)
        {
            FlashOctopus(yPos, xPos - 1);
        }
    }

    if (checkAbove)
    {
        // check UP
        input[yPos - 1][xPos] += 1;
        if (input[yPos - 1][xPos] > 9)
        {
            FlashOctopus(yPos - 1, xPos);
        }
    }

    if (checkRight)
    {
        // check RIGHT
        input[yPos][xPos + 1] += 1;
        if (input[yPos][xPos + 1] > 9)
        {
            FlashOctopus(yPos, xPos + 1);
        }
    }

    if (checkBelow)
    {
        // check DOWN
        input[yPos + 1][xPos] += 1;
        if (input[yPos + 1][xPos] > 9)
        {
            FlashOctopus(yPos + 1, xPos);
        }
    }

    if (checkAbove && checkLeft)
    {
        // check diagonally UP and to the LEFT
        input[yPos - 1][xPos - 1] += 1;
        if (input[yPos - 1][xPos - 1] > 9)
        {
            FlashOctopus(yPos - 1, xPos - 1);
        }
    }

    if (checkBelow && checkLeft)
    {
        // check diagonally DOWN and to the LEFT
        input[yPos + 1][xPos - 1] += 1;
        if (input[yPos + 1][xPos - 1] > 9)
        {
            FlashOctopus(yPos + 1, xPos - 1);
        }
    }

    if (checkAbove && checkRight)
    {
        // check diagonally UP and to the RIGHT
        input[yPos - 1][xPos + 1] += 1;
        if (input[yPos - 1][xPos + 1] > 9)
        {
            FlashOctopus(yPos - 1, xPos + 1);
        }
    }
    
    if (checkBelow && checkRight)
    {
        // check diagonally DOWN and to the RIGHT
        input[yPos + 1][xPos + 1] += 1;
        if (input[yPos + 1][xPos + 1] > 9)
        {
            FlashOctopus(yPos + 1, xPos + 1);
        }
    }
}
