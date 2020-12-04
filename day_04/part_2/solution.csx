#!/usr/bin/env dotnet script
#r "nuget: System.Text.RegularExpressions 4.3.1"

using System.Text.RegularExpressions;

List<string> ReadInput()
{
    return File.ReadAllLines("./input.txt").ToList();
}

struct Passport
{
    public Dictionary<string, string> Fields;
    public static Passport New(IEnumerable<string[]> fields)
    {
        var passport = new Passport() { Fields = new Dictionary<string, string>() };
        foreach (var field in fields)
        {
            passport.Fields[field[0]] = field[1];
        }
        return passport;
    }
}

List<Passport> ParseInput(List<string> lines)
{
    List<Passport> results = new List<Passport>();

    var iter = lines.GetEnumerator();
    while (iter.MoveNext())
    {
        // System.Console.WriteLine("New Passport");
        var line = iter.Current;
        // System.Console.WriteLine($"    Read line [{line}]");
        var fields = "";
        while (!string.IsNullOrWhiteSpace(line))
        {
            fields += " " + line;
            if (!iter.MoveNext()) goto end_of_passport;
            line = iter.Current;
            // System.Console.WriteLine($"    Read line [{line}]");
        }
    end_of_passport:
        // System.Console.WriteLine($"    Field [{fields}]");
        var validFields = fields
            .Split(" ")
            .Where(it => !string.IsNullOrWhiteSpace(it))
            .Select(it => it.Split(":"));
        // System.Console.WriteLine($"    Valid Fields [{validFields.Count()}]");
        results.Add(Passport.New(validFields));
    }

    return results;
}

bool TryGetIntField(Passport passport, string key, out int result)
{
    result = default(int);
    if (!passport.Fields.TryGetValue(key, out var field) || !int.TryParse(field, out result))
    {
        return false;
    }
    return true;
}
bool TryGetRegexField(Passport passport, string key, string regex, out GroupCollection result)
{
    result = default(GroupCollection);
    if (!passport.Fields.TryGetValue(key, out var field))
        return false;

    var match = new Regex(regex).Match(field);
    if (!match.Success)
        return false;

    result = match.Groups;
    return true;
}

bool IsValidPassport(Passport passport)
{
    if (!TryGetIntField(passport, "byr", out var intValue) || !(1920 <= intValue && intValue <= 2002))
        return false;
    if (!TryGetIntField(passport, "iyr", out intValue) || !(2010 <= intValue && intValue <= 2020))
        return false;
    if (!TryGetIntField(passport, "eyr", out intValue) || !(2020 <= intValue && intValue <= 2030))
        return false;

    if (!TryGetRegexField(passport, "hgt", @"^(\d+)((cm)|(in))$", out var groups))
        return false;
    if (groups[2].Value == "cm" && (!int.TryParse(groups[1].Value, out intValue) || !(150 <= intValue && intValue <= 193)))
        return false;
    else if (groups[2].Value == "in" && (!int.TryParse(groups[1].Value, out intValue) || !(59 <= intValue && intValue <= 76)))
        return false;

    if (!TryGetRegexField(passport, "hcl", @"^#[0-9a-f]{6}$", out _))
        return false;
    if (!TryGetRegexField(passport, "ecl", @"^(amb)|(blu)|(brn)|(gry)|(grn)|(hzl)|(oth)$$", out _))
        return false;
    if (!TryGetRegexField(passport, "pid", @"^\d{9}$", out _))
        return false;

    return true;
}

void main()
{
    var lines = ReadInput();
    var passports = ParseInput(lines);
    var numValid = passports.Where(IsValidPassport).Count();
    Console.WriteLine($"Found {passports.Count} passports, {numValid} are valid");
}

main();
