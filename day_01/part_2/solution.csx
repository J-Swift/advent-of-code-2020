#!/usr/bin/env dotnet script

List<int> ReadInput()
{
    return File.ReadAllLines("./input.txt").ToList().Select(it => int.Parse(it)).ToList();
}

(int, int, int) GetMatchingNumbers(List<int> numbers) {
    for (int idx = 0; idx < numbers.Count - 2; idx++)
    {
        var cur = numbers[idx];
        for (int nextIdx = idx + 1; nextIdx < numbers.Count - 1; nextIdx++)
        {
            var next = numbers[nextIdx];
            for (int nextIdx2 = nextIdx + 1; nextIdx2 < numbers.Count; nextIdx2++)
            {
                var next2 = numbers[nextIdx2];
                if (cur + next + next2 == 2020) return (cur, next, next2);
            }
        }
    }
    throw new Exception("No matches");
}

void main()
{
    var numbers = ReadInput();
    var matches = GetMatchingNumbers(numbers);
    Console.WriteLine($"Matched {matches.Item1} {matches.Item2} {matches.Item3}");
    Console.WriteLine(matches.Item1 * matches.Item2 * matches.Item3);
}

main();
