using static System.IO.File;

var inputPath = Helpers.Helpers.InputPath();
var enumarable = ReadLinesAsync(inputPath);
var input1 = new List<string>();
var isFirst = true;
await foreach (var line in enumarable)
{
    if (string.IsNullOrEmpty(line))
    {
        isFirst = false;
        continue;
    }
    if (isFirst)
        input1.Add(line);
}

var ranges = input1.Select(x => x.Split("-"))
    .Select(x =>
    (long.Parse(x[0]), long.Parse(x[1]))).ToList();

var distinct = new HashSet<long>();

var removals = new List<(long, long)>();
foreach (var outer in ranges)
{
    foreach (var inner in ranges.Where(x => x != outer))
    {
        if (outer.Item1 <= inner.Item1 && outer.Item2 >= inner.Item2)
        {
            removals.Add((inner.Item1, inner.Item2));
        }
    }
}
ranges.RemoveAll(z => removals.Any(x => x.Item1 == z.Item1 && z.Item2 == x.Item2));


Cleanup();
Console.WriteLine(ranges.Count);

Cleanup();
Console.WriteLine(ranges.Count);

Cleanup();
Console.WriteLine(ranges.Count);
Cleanup();
Console.WriteLine(ranges.Count);
Cleanup();
Console.WriteLine(ranges.Count);
Cleanup();

var sum = ranges.ToHashSet().Sum(x => x.Item2 - x.Item1 + 1);
Console.WriteLine(sum);



void Cleanup()
{
    var removals = new HashSet<(long, long)>();
    var additions = new HashSet<(long, long)>();
    foreach (var left in ranges)
    {
        foreach (var right in ranges.Where(x => x != left))
        {
            if (left.Item2 >= right.Item1 && left.Item1 <= right.Item1 && !removals.Contains(right) && !removals.Contains(left))
            {
                removals.Add(right);
                removals.Add(left);
                additions.Add((left.Item1, right.Item2));
            }
        }
    }

    var removed = ranges.RemoveAll(z => removals.Contains(z));
    ranges.AddRange(additions);
}

