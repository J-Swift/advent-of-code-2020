#!/usr/bin/env dotnet script

List<string> ReadInput()
{
    return File.ReadAllLines("./input.txt").ToList();
}

(int, int) ParseSeat(string encoded)
{
    var rowEnc = encoded.Substring(0, 7);
    var colEnc = encoded.Substring(7, 3);

    var row = Convert.ToInt32(rowEnc.Replace("F", "0").Replace("B", "1"), 2);
    var col = Convert.ToInt32(colEnc.Replace("L", "0").Replace("R", "1"), 2);
    return (row, col);
}

void main()
{
    var lines = ReadInput();
    var seats = lines.Select(ParseSeat);
    System.Console.WriteLine(seats.Max(it => 8*it.Item1 + it.Item2));
}

main();
