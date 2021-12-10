using System.Linq;

var input = System.IO.File.ReadAllLines(@".\input.11.1.txt").ToArray();
var chunkType1 = "()";
var chunkType2 = "[]";
var chunkType3 = "{}";
var chunkType4 = "<>";
var validChunks = new HashSet<string>()
{
    chunkType1,
    chunkType2,
    chunkType3,
    chunkType4,
};
var openChunkChars = "([{<";
var closeChunkChars = ")]}>";

int errorSum = 0;

Stack<char> openChunks = new Stack<char>();

var incompleteLines = new List<string>();

foreach (var line in input)
{
    bool corruptLine = false;
    foreach (var lineChar in line)
    {
        if (openChunkChars.Contains(lineChar))
        {
            openChunks.Push(lineChar);
        }
        else
        {
            char lastOpenedChunk = openChunks.Pop();
            string chunk = new (new char[]{ lastOpenedChunk, lineChar });
            if (!validChunks.Contains(chunk))
            {
                corruptLine = true;
                break;
            }
        }
    }
    if (!corruptLine)  // Then it is incomplete
    {
        incompleteLines.Add(line);
    }
}

long incompleteSum = 0;
List<long> incompleteSums = new ();

foreach (var incompleteLine in incompleteLines)
{
    openChunks = new Stack<char>();
    foreach (var lineChar in incompleteLine)
    {
        if (openChunkChars.Contains(lineChar))
        {
            openChunks.Push(lineChar);
        }
        else
        {
            char lastOpenedChunk = openChunks.Pop();            
        }
    }
    System.Console.WriteLine("chunks open :" + openChunks.Count());
    long incompleteSumLine = 0;
    foreach (var chunk in openChunks)
    {
        incompleteSumLine *= 5L;
        switch (chunk)
        {
            case '(': incompleteSumLine += 1L; break;
            case '[': incompleteSumLine += 2L; break;
            case '{': incompleteSumLine += 3L; break;
            case '<': incompleteSumLine += 4L; break;
        }
    }
    System.Console.WriteLine($"Incomplete sum line: {incompleteSumLine}");
    incompleteSums.Add(incompleteSumLine);
}

var array = incompleteSums.OrderBy(i => i).ToArray();
System.Console.WriteLine("middle sum: " + array[array.Length / 2]);

