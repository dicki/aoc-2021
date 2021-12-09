using System.Linq;

var input = System.IO.File.ReadAllLines(@".\input.9.0.txt").ToList();
var outputSums = 0;

foreach (var inputLine in input)
{
    var signalAndOutput = inputLine.Split(" | ");
    var signals = signalAndOutput.First().Split(" ").OrderBy(s => s).OrderBy(s => s.Length);
    var output = signalAndOutput.Last().Split(" ");

    string digit0 = string.Empty;
    string digit1 = string.Empty;
    string digit2 = string.Empty;
    string digit3 = string.Empty;
    string digit4 = string.Empty;
    string digit5 = string.Empty;
    string digit6 = string.Empty;
    string digit7 = string.Empty;
    string digit8 = string.Empty;
    string digit9 = string.Empty;


    while (digit9 == string.Empty ||
        digit6 == string.Empty ||
        digit0 == string.Empty ||
        digit2 == string.Empty ||
        digit3 == string.Empty ||
        digit5 == string.Empty)
    {
        foreach (var signal in signals)
        {
            if (signal.Length == 2) 
            {
                digit1 = new string(signal.ToArray());  // 1
            }
            if (signal.Length == 3)
            {
                digit7 = new string(signal.ToArray());  // 7
            }
            if (signal.Length == 4)
            {
                digit4 = new string(signal.ToArray());  // 4
            }
            if (signal.Length == 5) 
            {
                if (digit9 == string.Empty || digit6 == string.Empty || digit0 == string.Empty ) continue;

                if (CharactersExistsWithin(signal, digit6))
                {
                    digit5 = signal;
                }
                else if (CharactersExistsWithin(signal, digit9))
                {
                    digit3 = signal;
                }
                else
                {
                    digit2 = signal;
                }
            }
            if (signal.Length == 6)
            {
                if (digit9 == string.Empty && CharactersMissingWithin(signal, digit7+digit4) == 1)
                {
                    digit9 = signal;
                    continue;
                }
                if (signal != digit9 && digit0 == string.Empty && CharactersMissingWithin(digit1, signal) == 0)
                {
                    digit0 = signal;
                    continue;
                }

                if (digit0 != string.Empty && signal != digit0 && signal != digit9)
                {
                    digit6 = signal;
                    continue;
                }
            }
            if (signal.Length == 7)
            {
                digit8 = signal;
            }
        }
    }

    string decodedOutput = string.Empty;
    foreach (var outputNumber in output)
    {
        if (SameChars(outputNumber, digit0)) decodedOutput = decodedOutput + "0";
        if (SameChars(outputNumber, digit1)) decodedOutput = decodedOutput + "1";
        if (SameChars(outputNumber, digit2)) decodedOutput = decodedOutput + "2";
        if (SameChars(outputNumber, digit3)) decodedOutput = decodedOutput + "3";
        if (SameChars(outputNumber, digit4)) decodedOutput = decodedOutput + "4";
        if (SameChars(outputNumber, digit5)) decodedOutput = decodedOutput + "5";
        if (SameChars(outputNumber, digit6)) decodedOutput = decodedOutput + "6";
        if (SameChars(outputNumber, digit7)) decodedOutput = decodedOutput + "7";
        if (SameChars(outputNumber, digit8)) decodedOutput = decodedOutput + "8";
        if (SameChars(outputNumber, digit9)) decodedOutput = decodedOutput + "9";
    }
    outputSums += int.Parse(decodedOutput);
}

Console.WriteLine ("Total output sum: " +outputSums);
public bool SameChars(String firstStr, String secondStr) 
{
    // System.Console.WriteLine($"First: {new string(firstStr.OrderBy(c=>c).ToArray())} Second: {new string(secondStr.OrderBy(c=>c).ToArray())}");
    var first = new string(firstStr.OrderBy(c=>c).ToArray());
    var second = new string(secondStr.OrderBy(c=>c).ToArray());
    // System.Console.WriteLine(first + " " + second);
    var result = first.Equals(second);
    return result;
}

public bool CharactersExistsWithin(String characterString, String existsWithinString) 
{
  var first = characterString.ToCharArray().OrderBy(c => c);
  var second = existsWithinString.ToCharArray().OrderBy(c => c);
  bool results = false;
  foreach (var character in first)
  {
      results = existsWithinString.Contains(character);
      if (!results) break;
  }
  return results;
}

public int CharactersMissingWithin(String characterString, String existsWithinString) 
{
    var first = characterString.ToCharArray().Distinct().OrderBy(c => c).ToArray();
    var second = existsWithinString.ToCharArray().Distinct().OrderBy(c => c).ToArray();
    int missing = 0;
    foreach (var character in first)
    {
        if (!second.Contains(character)) missing++;
    }
    // System.Console.WriteLine($"{new string(first)} - {new string(second)} => {missing}");
    return missing;
}

public bool CharExists(string digit, char @char)
{
    return digit.Contains(@char);
}