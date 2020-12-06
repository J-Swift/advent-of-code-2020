#!/usr/bin/env dotnet script

List<string> ReadInput()
{
    return File.ReadAllLines("./input.txt").ToList();
}

List<List<string>> ParseInput(List<string> lines)
{
    List<List<string>> results = new List<List<string>>();

    var iter = lines.GetEnumerator();
    while (iter.MoveNext())
    {
        var line = iter.Current;
        var fields = new List<string>();
        while (!string.IsNullOrWhiteSpace(line))
        {
            fields.Add(line.Trim());
            if (!iter.MoveNext()) break;
            line = iter.Current;
        }
        results.Add(fields);
    }

    return results;
}

int GetUbiquitousAnswerCount(List<string> answers)
{
    return answers
        .Select(it => it.ToHashSet())
        .Aggregate((p1answers, p2answers) => { p1answers.IntersectWith(p2answers); return p1answers; })
        .Count();
}

void main()
{
    var lines = ReadInput();
    var answers = ParseInput(lines);
    System.Console.WriteLine(answers.Select(GetUbiquitousAnswerCount).Sum());
}

main();
