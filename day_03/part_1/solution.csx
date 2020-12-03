#!/usr/bin/env dotnet script

List<string> ReadInput()
{
    return File.ReadAllLines("./input.txt").ToList();
}

List<List<String>> ParseInput(List<string> lines)
{
    return lines.Select(it => it.ToCharArray().Select(char.ToString).ToList()).ToList();
}

bool IsTree(string candidate)
{
    return candidate == "#";
}

void PrintBoard(List<List<string>> map, (int, int) curSpace)
{
    System.Console.WriteLine("-------------------");
    System.Console.WriteLine($"[x {curSpace.Item1}] [y {curSpace.Item2}]");
    for (int y = 0; y < map.Count; y++)
    {
        for (int x = 0; x < map[y].Count; x++)
        {
            var curChar = map[y][x].ToString();
            Console.Write(x == curSpace.Item1 && y == curSpace.Item2 ? "O" : curChar);
        }
        System.Console.WriteLine();
    }
    ReadLine();
}

void main()
{
    var lines = ReadInput();
    var map = ParseInput(lines);
    var treesFound = 0;
    (int X, int Y) offset = (3, 1);
    int curX = offset.X, curY = offset.Y;
    while (curY < map.Count) {
        var curSpace = map[curY][curX];
        // PrintBoard(map, (curX, curY));
        if (IsTree(curSpace)) treesFound += 1;
        curX = (curX + offset.X) % map[curY].Count;
        curY += offset.Y;
    }
    Console.WriteLine($"Found {treesFound} trees");
}

main();
