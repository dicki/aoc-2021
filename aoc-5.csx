    using System.Numerics;
    using System.Linq;

var drawInput = System.IO.File.ReadAllLines(@".\input.5.1.txt").First();
var bingoBoardInput = System.IO.File.ReadAllLines(@".\input.5.2.txt").ToList();

public class BingoNumber
{
    public int Number;
    public int Column;
    public int Row;
    public bool Marked;
}

public class BingoBoard
{
    public bool Bingo;
    public BingoNumber[,] BingoNumbers = new BingoNumber[5,5];
    public int[] ColumnBingoCounter = { 0,0,0,0,0 };
    public int[] RowBingoCounter = { 0,0,0,0,0 };
}

var bingoBoards = new List<BingoBoard>();
var bingoBoard = new BingoBoard();
bingoBoards.Add(bingoBoard);

// Create bingo boards
for (int yPos = 0; yPos < bingoBoardInput.Count; yPos++)
{
    int yPosBoard = yPos % 5;
    string line = bingoBoardInput[yPos];
    if (string.IsNullOrWhiteSpace(line))
    {
        // new bingo board
        bingoBoard = new BingoBoard(); //new BingoNumber[5,5];
        bingoBoards.Add(bingoBoard);
        continue;
    }
    string[] columns = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    for (int xPos = 0; xPos < columns.Length; xPos++)
    {
        if (bingoBoard.BingoNumbers[xPos,yPosBoard] == default) bingoBoard.BingoNumbers[xPos,yPosBoard] = new BingoNumber();
        string number = columns[xPos];
        bingoBoard.BingoNumbers[xPos,yPosBoard].Number = int.Parse(number);
        bingoBoard.BingoNumbers[xPos,yPosBoard].Row = yPosBoard;
        bingoBoard.BingoNumbers[xPos,yPosBoard].Column = xPos;
    }
}

var bingo = false;
List<int> bingoSeq = new();
BingoBoard winningBoard = null;
List<BingoBoard> winningBoardOrder = new ();
int lastDrawnNumber = -1;
foreach (var numberDrawnString in drawInput.Split(","))
{
    int numberDrawn = int.Parse(numberDrawnString);
    System.Console.WriteLine("number drawn: " + numberDrawn);
    foreach (BingoBoard board in bingoBoards)
    {
        if (board.Bingo) continue;
        var bingoNumber = board.BingoNumbers.Cast<BingoNumber>().ToArray().SingleOrDefault(bn => bn.Number == numberDrawn);
        if (bingoNumber == default) continue;
        if (bingoNumber.Column == 3)
        {
            System.Console.WriteLine("Number found: " + bingoNumber.Number +" flat board: "+ string.Join(' ',board.BingoNumbers.Cast<BingoNumber>().ToArray().Select(bn => bn.Number)));
            System.Console.WriteLine("Marked in Column 3: " +board.ColumnBingoCounter[bingoNumber.Column]);
        }
        lastDrawnNumber = bingoNumber.Number;

        bingoNumber.Marked = true;
        board.ColumnBingoCounter[bingoNumber.Column] += 1;
        board.RowBingoCounter[bingoNumber.Row] += 1;

        if (board.ColumnBingoCounter.Any(c => c == 5))
        {
            for (int i = 0; i < 5; i++)
            {
                bingoSeq.Add(board.BingoNumbers[bingoNumber.Column,i].Number);
            }
            board.Bingo = true;
            winningBoard = board;
            winningBoardOrder.Add(board);
            // System.Console.WriteLine($"Column Bingo: {string.Join(' ', bingoSeq.ToArray())}");
            bingoSeq.Clear();
            bingo = true;
            // break;
        }

        if (board.RowBingoCounter.Any(r => r == 5))
        {
            for (int i = 0; i < 5; i++)
            {
                bingoSeq.Add(board.BingoNumbers[i,bingoNumber.Row].Number);
            }
            board.Bingo = true;
            winningBoard = board;
            winningBoardOrder.Add(board);
            // System.Console.WriteLine($"Row Bingo: {string.Join(' ', bingoSeq.ToArray())}");
            bingoSeq.Clear();
            bingo = true;
            // break;
        }
    }
    // if (bingo) break;
}

if (bingo)
{
    var nonWinningNumbers = winningBoardOrder.Last().BingoNumbers.Cast<BingoNumber>().ToArray().Where(bn => !bn.Marked).Select(bn=> bn.Number);
    System.Console.WriteLine("last drawn number" + lastDrawnNumber);
    System.Console.WriteLine("Non winning numbers" + nonWinningNumbers.Sum());
    System.Console.WriteLine("Result: " + nonWinningNumbers.Sum() * lastDrawnNumber);
}

// foreach (var board in bingoBoards)
// {

//     for (int yPos = 0; yPos < 5; yPos++)
//     {
//         int yPosBoard = yPos % 5;
//         for (int xPos = 0; xPos < 5; xPos++)
//         {
//             Console.Write(board.BingoNumbers[xPos,yPosBoard].Number+" ");
//         }
//         Console.WriteLine("");
//     }
//     Console.WriteLine("---------");
// }

