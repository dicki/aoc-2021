var stringLines = System.IO.File.ReadAllLines(@".\input.txt");
var intLines = stringLines.Select(line => int.Parse(line));
int previousReadingSum = int.MinValue;
int currentReadingSum = int.MinValue;
int noOfReadingsLargerThanPrevious = 0;

var remainingLines = intLines;

while (remainingLines.Any())
{
    var currentReadingSum = remainingLines.Take(3).Sum();
    if (previousReadingSum != int.MinValue && previousReadingSum < currentReadingSum)
    {
        noOfReadingsLargerThanPrevious++;
    }
    previousReadingSum = currentReadingSum;
    remainingLines = remainingLines.Skip(1);
}

Console.WriteLine(noOfReadingsLargerThanPrevious);
