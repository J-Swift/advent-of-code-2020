#!/usr/bin/env dotnet script

List<long> ReadInput()
{
    return File.ReadAllLines("./input.txt").Select(long.Parse).ToList();
}

bool IsValidNumber(long number, List<long> numbers)
{
    var sums = new HashSet<long>();
    for (int i = 0; i < numbers.Count - 1; i++)
    {
        for (int j = i + 1; j < numbers.Count; j++)
        {
            sums.Add(numbers[i] + numbers[j]);
        }
    }
    return sums.Contains(number);
}

void main()
{
    var numbers = ReadInput();
    var preamble = 25;
    for (int i = preamble; i < numbers.Count; i++)
    {
        if (!IsValidNumber(numbers[i], numbers.GetRange(i - preamble, preamble))) {
            System.Console.WriteLine($"Not valid [idx {i}] [{numbers[i]}]");
            break;
        }
    }
}

main();
