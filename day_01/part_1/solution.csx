#!/usr/bin/env dotnet script

List<int> ReadInput()
{
    return File.ReadAllLines("./input.txt").ToList().Select(it => int.Parse(it)).ToList();
}

(int, int) GetMatchingNumbers(List<int> numbers) {
    for (int idx = 0; idx < numbers.Count - 1; idx++)
    {
        var cur = numbers[idx];
        for (int nextIdx = idx + 1; nextIdx < numbers.Count; nextIdx++)
        {
            var next = numbers[nextIdx];
            if (cur + next == 2020) return (cur, next);
        }
    }
    throw new Exception("No matches");
}

void main()
{
    var numbers = ReadInput();
    var matches = GetMatchingNumbers(numbers);
    Console.WriteLine($"Matched {matches.Item1} {matches.Item2}");
    Console.WriteLine(matches.Item1 * matches.Item2);
}

main();
