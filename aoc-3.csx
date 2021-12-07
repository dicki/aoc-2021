using System.Numerics;

var lines = System.IO.File.ReadAllLines(@".\input.3.txt");

int posX = 0;
int posY = 0;
int aim = 0;

foreach (string line in lines)
{
    var command = line.Split(" ")[0];
    var units = int.Parse(line.Split(" ")[1]);

    Console.WriteLine($"{command} -> {units}");

    switch (command)
    {
        case "forward": posX += units; posY += aim * units; break;
        case "down": /* posY += units; */ aim += units; break;
        case "up": /* posY -= units; */ aim -= units; break;
    }
    Console.WriteLine($"x: {posX} y: {posY} aim: {aim}");
    Console.WriteLine();
}

Console.WriteLine($"x: {posX} y: {posY} aim: {aim} - {posX * posY}");