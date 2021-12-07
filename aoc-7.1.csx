    using System.Numerics;
    using System.Linq;

var input = System.IO.File.ReadAllLines(@".\input.7.txt").First().Split(',');
var daysToSimulate = 256;

var fishPool = input.Select(fish => int.Parse(fish)).ToList();
long fishCounter = 0L;
fishCounter += fishPool.LongCount();
System.Console.WriteLine($"Total fish in the beginning: {fishCounter}");

Dictionary<int,long> pool = new ();
Dictionary<int,long> poolChange = new ();
int[] fishLifeCycle = {0,1,2,3,4,5,6,7,8};
foreach (var flc in fishLifeCycle)
{
    System.Console.WriteLine(flc);
    pool.Add(flc,0);
    poolChange.Add(flc,0);
}
fishPool.ForEach(fish => pool[fish] += 1);

int newFishCounter = 0;

for (int day = 1; day <= daysToSimulate; day++)
{
    // System.Console.WriteLine($"Day\t{day}:\t{string.Join(',', fishPool)}");
    for (int fish = 0; fish < pool.Count(); fish++)
    {
        if (fish == 0)
        {
            poolChange[6] = pool[fish];  // reset all fish at counter = 0
            poolChange[8] = pool[fish];  // add new fish for each fish at counter = 0
            poolChange[0] -= pool[fish];
        }
        else
        {
            poolChange[fish] -= pool[fish];
            poolChange[fish-1] += pool[fish];
        }
    }
    
    // Apply pool changes to pool
    for (int fish = 0; fish < pool.Count(); fish++)
    {
        pool[fish] += poolChange[fish];
        poolChange[fish] = 0;
    }
}
System.Console.WriteLine($"Total fish after {daysToSimulate} days: {pool.Sum(p=>p.Value)}");