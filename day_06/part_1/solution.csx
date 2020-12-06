#!/usr/bin/env dotnet script

List<string> ReadInput()
{
    return File.ReadAllLines("./input.txt").ToList();
}

List<string> ParseInput(List<string> lines)
{
    List<string> results = new List<string>();

    var iter = lines.GetEnumerator();
    while (iter.MoveNext())
    {
        var line = iter.Current;
        var fields = "";
        while (!string.IsNullOrWhiteSpace(line))
        {
            fields += line;
            if (!iter.MoveNext()) break;
            line = iter.Current;
        }
        results.Add(fields.Trim());
    }

    return results;
}

int GetAnswerCount(string answers)
{
    // https://stackoverflow.com/a/23369865/1273175
    return answers.Distinct().Count();
}

void main()
{
    var lines = ReadInput();
    var answers = ParseInput(lines);
    System.Console.WriteLine(answers.Select(GetAnswerCount).Sum());
}

main();
