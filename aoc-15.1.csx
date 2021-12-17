using System.Linq;

var input = File.ReadAllLines(@".\input.15.0.txt");

var template = input.First();

var pairInsertions = input
    .Where(i => i.Contains(" -> "))
    .Select(i => i.Split(" -> "))
    .ToDictionary(i => i[0], i => i[1]);


var charCounter = new Dictionary<char, long>();
var pairCounter = new Dictionary<string, long>();
for (int i = 0; i < template.Length - 1; i++)
{
    string pair = template.Substring(i,2);
    if (pairCounter.ContainsKey(pair))
      pairCounter[pair] += 1;
    else
      pairCounter[pair] = 1;
}


int stepsToPerform = 40;
var newPairCounter = new Dictionary<string, long>();

for (int step = 0; step < stepsToPerform; step++)
{
    var pairs = pairCounter.Keys.ToList();
    var pairsCount = pairCounter.Values.ToList();
    for (int i = 0; i < pairs.Count; i++)
    {
        string ab = pairs[i];
        long pc = pairsCount[i];
        if (pc < 1) continue;
        var a = ab[0];
        var b = ab[1];
        var c = pairInsertions[ab];
        var ac = $"{a}{c}";
        var cb = $"{c}{b}";
        var acb = $"{a}{c}{b}";

        pairCounter[ab] -= pc;
        if (!pairCounter.TryAdd(ac, pc)) pairCounter[ac] += pc;
        if (!pairCounter.TryAdd(cb, pc)) pairCounter[cb] += pc;
    }
    
}

var aList = pairCounter.Select(p => new { @Char = p.Key[0], Count = p.Value}).GroupBy(key => key.Char, element => element.Count, (@Char, Count) => new { @Char, Count = Count.Sum() });
var bList = pairCounter.Select(p => new { @Char = p.Key[1], Count = p.Value}).GroupBy(key => key.Char, element => element.Count, (@Char, Count) => new { @Char, Count = Count.Sum() });
var orderedCounter = aList.Join(bList, a => a.Char, b => b.Char, (a,b) => new { a.Char, Count = Math.Max(a.Count,b.Count)}).OrderBy(ab => ab.Count);
var minChar = orderedCounter.First();
var min = minChar.Count;
var maxChar = orderedCounter.Last();
var max = maxChar.Count;
WriteLine($"{maxChar.Char} - {minChar.Char}");
WriteLine($"{max} - {min} = {max - min}");
WriteLine("---");

foreach (var c in orderedCounter)
{
    WriteLine($"{c.Char} = {c.Count}");
}