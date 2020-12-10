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

List<long> GetSumChain(long target, List<long> numbers)
{
    for (int i = 0; i < numbers.Count; i++)
    {
        long sum = 0;
        for (int j = i; j < numbers.Count; j++)
        {
            sum += numbers[j];
            if (sum == target) return numbers.GetRange(i, j - i + 1);
            if (sum > target) break;
        }
    }
    return null;
}

void main()
{
    var numbers = ReadInput();
    var preamble = 25;
    for (int i = preamble; i < numbers.Count; i++)
    {
        if (!IsValidNumber(numbers[i], numbers.GetRange(i - preamble, preamble)))
        {
            var result = GetSumChain(numbers[i], numbers);
            System.Console.WriteLine(result.Min() + result.Max());
            break;
        }
    }
}

main();
