using System;
using System.Collections.Generic;
using System.Linq;
using static System.IO.File;

var inputPath = Helpers.Helpers.InputPath();
var input = (await ReadAllLinesAsync(inputPath))[0];

var invalids = new List<long>();

foreach (var range in input.Split(","))
{
    var start = long.Parse(range.Split("-")[0]);
    var end = long.Parse(range.Split("-")[1]);
    
    for (long i = start; i <= end; i++)
    {
        var s = i.ToString();

        for (var z = 2; z <= s.Length; z++)
        {
            if (s.Length % z != 0) continue;

            var partLength = s.Length / z;
            var parts = new List<string>();

            for (var p = 0; p < z; p++)
            {
                parts.Add(s.Substring(p * partLength, partLength));
            }

            if (parts.All(part => part == parts[0]))
            {
                invalids.Add(i);
                break;
            }
        }
    }
}
var value = invalids.Sum();
Console.WriteLine(value);
