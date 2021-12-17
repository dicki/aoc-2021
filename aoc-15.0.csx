using System.Linq;

var input = File.ReadAllLines(@".\input.15.0.txt");

var template = input.First();

System.IO.File.WriteAllText(@".\temp.15.0.txt", template);

var pairInsertions = input
    .Where(i => i.Contains(" -> "))
    .Select(i => i.Split(" -> "))
    .ToDictionary(i => i[0], i => i[1]);


var charCounter = new Dictionary<char, int>();
int stepsToPerform = 40;
string readFileName, writeFileName;

var fs1 = new MemoryStream();
fs1.Write(Encoding.UTF8.GetBytes(template));
fs1.Seek(0, SeekOrigin.Begin);
var fs2 = new MemoryStream();
var readBuffer = new byte[2];
for (int step = 0; step < stepsToPerform; step++)
{
    readFileName = @$".\temp.15.{step % 2}.txt";
    writeFileName = @$".\temp.15.{(step + 1) % 2}.txt";
    
    // using var fs1 = new FileStream(readFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 1024 * 1024 * 25, true);
    // using var fs1 = File.Open(readFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
    // using var fs2 = new StreamWriter(File.Open(writeFileName, FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite));
    // using var fs2 = new StreamWriter(new FileStream(writeFileName, FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite, 1024 * 1024 * 25, true));

    WriteLine($"Step {step} Count: {fs1.Length}");
    var maxReadLength = 2;
    for (int templatePos = 0; templatePos < fs1.Length - 1; templatePos++)
    {
        fs1.Read(readBuffer, 0, (int)maxReadLength);
        fs1.Seek(-1, SeekOrigin.Current);

        string pair = new (new[]{(char)readBuffer[0], (char)readBuffer[1]});
        var insertPair = Pair(pair, templatePos);

        if (insertPair.Length == 0) break;
        fs2.Write(Encoding.UTF8.GetBytes(insertPair), 0, insertPair.Length);
    }
    fs1.Seek(0, SeekOrigin.Begin);
    fs2.WriteTo(fs1);
    fs1.Seek(0, SeekOrigin.Begin);
    fs2.Seek(0, SeekOrigin.Begin);
}

fs1.Seek(0, SeekOrigin.Begin);
while(fs1.Position < fs1.Length)
{
    char @char = (char)fs1.ReadByte();
    CountChars(@char);
}

char minChar = charCounter.Min(c => c.Key);
int min = charCounter.Min(c => c.Value);
char maxChar = charCounter.Max(c => c.Key);
int max = charCounter.Max(c => c.Value);
WriteLine($"{maxChar} - {minChar}");
WriteLine($"{max} - {min} = {max - min}");

char[] Pair(string pair, int templatePos)
{
    if (pair[0] == 0 || pair[1] == 0) return new char[0];

    string insert = pairInsertions[pair];
    if (templatePos > 0)
      return new char[]{ insert[0], pair[1] };
    else 
      return new char[]{ pair[0], insert[0], pair[1] };
}

void CountChars(char @char)
{
    if (charCounter.ContainsKey(@char))
    {
        charCounter[@char] += 1;
    }
    else
    {
        charCounter[@char] = 1;
    }
}