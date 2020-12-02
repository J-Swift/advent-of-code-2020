#!/usr/bin/env dotnet script
#r "nuget: System.Text.RegularExpressions 4.3.1"

using System.Text.RegularExpressions;

IList<string> ReadInput()
{
    return File.ReadAllLines("./input.txt").ToList();
}

struct PasswordPolicy
{
    public int min;
    public int max;
    public string required;
    public string password;
}

Regex r = new Regex(@"(\d+)-(\d+) (\w): (\w+)", RegexOptions.IgnoreCase);

IList<PasswordPolicy> ParseInput(IList<string> lines)
{
    return lines.Select(it =>
    {
        var matches = r.Match(it);
        return new PasswordPolicy
        {
            min = int.Parse(matches.Groups[1].ToString()),
            max = int.Parse(matches.Groups[2].ToString()),
            required = matches.Groups[3].ToString(),
            password = matches.Groups[4].ToString(),
        };
    }).ToList();
}

bool IsValidPolicy(PasswordPolicy policy) {
    var matchesMin = policy.password[policy.min-1] == policy.required[0];
    var matchesMax = policy.password[policy.max-1] == policy.required[0];
    return matchesMin ^ matchesMax;
}

void main()
{
    var lines = ReadInput();
    var policies = ParseInput(lines);
    System.Console.WriteLine(policies.Aggregate(0, (memo, policy) => memo + (IsValidPolicy(policy) ? 1 : 0)));
}

main();
