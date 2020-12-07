#!/usr/bin/env dotnet script
#r "nuget: System.Text.RegularExpressions 4.3.1"

using System.Text.RegularExpressions;

List<string> ReadInput()
{
    return File.ReadAllLines("./input.txt").ToList();
}

Dictionary<string, HashSet<string>> ParseInput(List<string> lines)
{
    var result = new Dictionary<string, HashSet<string>>();

    var regex = new Regex(@"^(\w+ \w+) bags contain (.+)\.");
    var matchRegex = new Regex(@"\d+ (\w+ \w+) bag");

    foreach (var line in lines)
    {
        var dest = new HashSet<string>();

        var groups = regex.Match(line).Groups;
        var source = groups[1].Value;
        if (groups[2].Value != "no other bags") {
            var eachMatch = groups[2].Value.Split(", ");
            foreach (var match in eachMatch)
            {
                dest.Add(matchRegex.Match(match).Groups[1].Value);
            }
        }
        result[source] = dest;
    }

    return result;
}

bool CanContain(Dictionary<string, HashSet<string>> mappings, string source, string target, Dictionary<string, bool> memo = null)
{
    memo = memo ?? new Dictionary<string, bool>();
    if (memo.ContainsKey(source)) return memo[source];

    if (mappings[source].Count == 0) {
        memo[source] = false;
        return false;
    }

    if (mappings[source].Contains(target))
    {
        memo[source] = true;
        return true;
    }

    if (mappings[source].Any(it => CanContain(mappings, it, target, memo)))
    {
        memo[source] = true;
        return true;
    }

    memo[source] = false;
    return false;
}

void main()
{
    var lines = ReadInput();
    var mappings = ParseInput(lines);
    System.Console.WriteLine(mappings.Where(it => CanContain(mappings, it.Key, "shiny gold")).Count());
}

main();
