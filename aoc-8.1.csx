    using System.Numerics;
    using System.Linq;

var input = System.IO.File.ReadAllLines(@".\input.8.0.txt").First().Split(',');
var crabs = input.Select(i => int.Parse(i));
var startCrab = crabs.Min();
var endCrab = crabs.Max();
var crabFuelCost = new List<int>();

var stopwatch = Stopwatch.StartNew();

Parallel.ForEach(Enumerable.Range(
    startCrab, endCrab), 
    (crab) =>
    {
        var crabFuel = CalculateCrabFuel(crabs, crab);
        crabFuelCost.Add(crabFuel);
    });

stopwatch.Stop();
System.Console.WriteLine($"Lowest fuel cost: {crabFuelCost.Min()}");
System.Console.WriteLine($"Total runtime: {stopwatch.ElapsedMilliseconds}");

public int CalculateCrabFuel(IEnumerable<int> crabs, int destinationPosition)
{
    return crabs.Sum(c =>
    {
       
        int min = Math.Min(destinationPosition, c);
        int max = Math.Max(destinationPosition, c);
        int n = max - min;
        return n * (n + 1) / 2;
    });
}