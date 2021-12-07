using System.Numerics;

var linesOxygen = System.IO.File.ReadAllLines(@".\input.4.txt").ToList();
var linesCo2 = System.IO.File.ReadAllLines(@".\input.4.txt").ToList();
var lineLength = linesOxygen.First().Length;
Console.WriteLine($"Line length: {lineLength}");
var bitCountersOxygen = new List<(int TrueBits, int FalseBits, bool Done)>(lineLength);
var bitCountersCo2 = new List<(int TrueBits, int FalseBits, bool Done)>(lineLength);

for (int i = 0;i < lineLength; i++)
{
    bitCountersOxygen.Add(new ());
    bitCountersCo2.Add(new ());
}

var halfwayLine = linesOxygen.Count() / 2;
var results = new List<char>();
var negativeResults = new List<char>();


Console.WriteLine($"Total linesOxygen: {linesOxygen.Count()} Halfway line: {halfwayLine}");

for (int xPos=0; xPos<lineLength; xPos++)
{

    int yPos = 0;
    while (linesOxygen.Any())
    {
        var bit = linesOxygen[yPos][xPos];
        // Console.WriteLine($"linesOxygen: {linesOxygen.Count() - 1} yPos: {yPos}");
        
        var bitCounter = bitCountersOxygen[xPos];

        if (bit.Equals('1'))
        {
            bitCounter.TrueBits += 1;
        }
        else
        {
            bitCounter.FalseBits += 1;
        }
        bitCountersOxygen[xPos] = bitCounter;
        
        if (yPos == linesOxygen.Count() - 1)
        {
            if (bitCounter.TrueBits >= bitCounter.FalseBits)
            {
                linesOxygen = linesOxygen.Where(l => 
                {
                    // Console.WriteLine(l[xPos]);
                    return l[xPos] == '1';
                }).ToList();
                results.Add('1');
            }
            else
            {
                linesOxygen = linesOxygen.Where(l =>
                {
                    // Console.WriteLine(l[xPos]);
                    return l[xPos] == '0';
                }).ToList();
                results.Add('0');
            }
            break;
        }
        else
        {
            yPos++;
        }
    }

    yPos = 0;
    while (linesCo2.Any())
    {
        if (linesCo2.Count() == 1) break;

        var bit = linesCo2[yPos][xPos];
        
        var bitCounter = bitCountersCo2[xPos];

        if (bit.Equals('1'))
        {
            bitCounter.TrueBits += 1;
        }
        else
        {
            bitCounter.FalseBits += 1;
        }
        bitCountersCo2[xPos] = bitCounter;
        
        if (yPos == linesCo2.Count() - 1)
        {
            if (bitCounter.TrueBits >= bitCounter.FalseBits)
            {
                linesCo2 = linesCo2.Where(l => 
                {
                    Console.WriteLine(l);
                    return l[xPos] == '0';
                }).ToList();
            }
            else
            {
                linesCo2 = linesCo2.Where(l =>
                {
                    Console.WriteLine(l);
                    return l[xPos] == '1';
                }).ToList();
            }
            Console.WriteLine("------");
            break;
        }
        else
        {
            yPos++;
        }
    }
}

var resultsString = new string(results.ToArray());
var resultsInt = Convert.ToInt32(resultsString, 2);
Console.WriteLine("last line co2: " + linesCo2.First());
negativeResults.AddRange(linesCo2.First());
var negativeResultsString = new string(negativeResults.ToArray());
var negativeResultsInt = Convert.ToInt32(negativeResultsString, 2);

Console.WriteLine(resultsString);
Console.WriteLine(resultsInt);

Console.WriteLine(negativeResultsString);
Console.WriteLine(negativeResultsInt);

Console.WriteLine(resultsInt * negativeResultsInt);
