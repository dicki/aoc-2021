    using System.Numerics;
    using System.Linq;

var input = System.IO.File.ReadAllLines(@".\input.7.test.txt").First().Split(',');
var daysToSimulate = 256;

var fishPools = new List<List<byte>>();
var firstFishPool = input.Select(fish => byte.Parse(fish)).ToList();
fishPools.Add(firstFishPool);
long fishCounter = 0L;
fishCounter += firstFishPool.LongCount();
System.Console.WriteLine($"Total fish in the beginning: {fishCounter}");

int poolSize = int.MaxValue / 2;

for (int day = 1; day <= daysToSimulate; day++)
{
    // System.Console.WriteLine($"Day\t{day}:\t{string.Join(',', fishPool)}");
    int fishPoolCounter = fishPools.Count();
    for (int p = 0; p < fishPoolCounter; p++)
    {
        var fishPool = fishPools[p];
        var newfishInThisPool = fishPools.Last();
        var fishPoolCount = fishPool.Count();
        if (fishPoolCount > poolSize)
        {
            newfishInThisPool = new List<byte>();
            fishPools.Add(newfishInThisPool);
        }
        for (int i = 0; i < fishPoolCount; i++)
        {
            byte fish = fishPool[i];
            if (fish == 0)
            {
                fishCounter++;
                newfishInThisPool.Add(8);
                fishPool[i] = 6;
            }
            else
            {
                fish--;
                fishPool[i] = fish;
            }
        }
    }
}
System.Console.WriteLine($"Total fish after {daysToSimulate} days: {fishCounter}");