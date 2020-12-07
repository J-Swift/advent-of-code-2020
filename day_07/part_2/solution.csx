#!/usr/bin/env dotnet script
#r "nuget: System.Text.RegularExpressions 4.3.1"

using System.Text.RegularExpressions;

List<string> ReadInput()
{
    return File.ReadAllLines("./input.txt").ToList();
}

Dictionary<string, Dictionary<string, int>> ParseInput(List<string> lines)
{
    var result = new Dictionary<string, Dictionary<string, int>>();

    var regex = new Regex(@"^(\w+ \w+) bags contain (.+)\.");
    var matchRegex = new Regex(@"(\d+) (\w+ \w+) bag");

    foreach (var line in lines)
    {
        var dest = new Dictionary<string, int>();

        var groups = regex.Match(line).Groups;
        var source = groups[1].Value;
        if (groups[2].Value != "no other bags") {
            var eachMatch = groups[2].Value.Split(", ");
            foreach (var match in eachMatch)
            {
                var matchGroups = matchRegex.Match(match).Groups;
                var quantity = int.Parse(matchGroups[1].Value);
                var color = matchGroups[2].Value;
                dest[color] = quantity;
            }
        }
        result[source] = dest;
    }

    return result;
}

int NumBagsContained(Dictionary<string, Dictionary<string, int>> mappings, string source, Dictionary<string, int> memo = null)
{
    memo = memo ?? new Dictionary<string, int>();
    if (memo.ContainsKey(source)) return memo[source];

    var total = 1 + mappings[source].Select(it => it.Value * NumBagsContained(mappings, it.Key)).Sum();
    memo[source] = total;
    return total;
}

void main()
{
    var lines = ReadInput();
    var mappings = ParseInput(lines);
    System.Console.WriteLine(NumBagsContained(mappings, "shiny gold") - 1);
}

main();
