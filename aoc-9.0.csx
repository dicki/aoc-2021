using System.Linq;

string digit0 = "abcefg";
string digit1 = "cf";
string digit2 = "acdeg";
string digit3 = "acdfg";
string digit4 = "bcdf";
string digit5 = "abdfg";
string digit6 = "abdefg";
string digit7 = "acf";
string digit8 = "abcdefg";
string digit9 = "abcdfg";

const int length_2 = 1; // 1
const int length_3 = 1; // 7
const int length_4 = 1; // 4
const int length_5 = 3; // 2 3 5
const int length_6 = 3; // 0 6 9
const int length_7 = 1; // 8 

var input = System.IO.File.ReadAllLines(@".\input.9.0.txt").Select(line => line.Split(" | ").Last()).ToList();
System.Console.WriteLine($"Unique values: {input.SelectMany(i => i.Split(" ")).Count(i => i.Length < 5 || i.Length == 7)}");