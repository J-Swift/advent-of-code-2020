#!/usr/bin/env dotnet script

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

bool IsValidPassport(Passport passport)
{
    var requiredFields = new List<string>(){
        "byr",
        "iyr",
        "eyr",
        "hgt",
        "hcl",
        "ecl",
        "pid",
    };
    return requiredFields.All(passport.Fields.ContainsKey);
}

void main()
{
    var lines = ReadInput();
    var passports = ParseInput(lines);
    var numValid = passports.Where(IsValidPassport).Count();
    Console.WriteLine($"Found {passports.Count} passports, {numValid} are valid");
}

main();
