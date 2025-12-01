using System;
using static System.IO.File;

var state = 50;
var zeros = 0;

var inputPath = Helpers.Helpers.InputPath();
var input = await ReadAllLinesAsync(inputPath);

foreach (var s in input)
{
    var dir = s[0] == 'L' ? -1 : 1;
    var amt = int.Parse(s[1..]);

    for (int i = 0; i < Math.Abs(amt); i++)
    {
        state += dir;
        if (state % 100 == 0)
        {
            zeros++;
        }
    }
}

Console.WriteLine(zeros);


