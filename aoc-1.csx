var lines = System.IO.File.ReadAllLines(@".\input.txt");

int previousReading = int.MinValue;
int noOfReadingsLargerThanPrevious = 0;
foreach (var line in lines)
{
    int currentReading = int.Parse(line);

    if (previousReading != int.MinValue && currentReading > previousReading)
    {
        noOfReadingsLargerThanPrevious++;
    }
    previousReading = currentReading;
}

Console.WriteLine(noOfReadingsLargerThanPrevious);
