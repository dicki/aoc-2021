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
var openChunkChars = "({[<";

int errorSum = 0;

Stack<char> openChunks = new Stack<char>();


foreach (var line in input)
{
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
                switch (lineChar)
                {
                    case ')': errorSum += 3; break;
                    case ']': errorSum += 57; break;
                    case '}': errorSum += 1197; break;
                    case '>': errorSum += 25137; break;
                }
            }
        }
    }
}

System.Console.WriteLine($"Total error sum: {errorSum}");
