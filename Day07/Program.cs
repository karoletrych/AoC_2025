using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Helpers;
using static System.IO.File;

var input = await Helpers.Helpers.Load2DArray();
var start = input.FindItem('S');
var length = input.GetLength(1);

int universesCount = 0;

var visited = new Dictionary<(int, int), long>();
// Console.WriteLine( CountSplit(start.x, start.y));


var splits = CountSplit(start.x, start.y);
Console.WriteLine(splits);
return;
// Console.WriteLine(universesCount);

long CountSplit(int yOrig, int x)
{
    if (visited.ContainsKey((yOrig, x)))
    {
        return visited[(yOrig, x)];
    } 
    
    int y = yOrig;
    while (true)
    {
        // if (input.HasIndex(x, y) && input[y, x] == '.')
        // {
        //     input[y, x] = 'X';
        //     Console.WriteLine($"{y}, {x}");
        // }

        if (y > length)
        {
            universesCount++;
            return 1;
        }
        if (input[y, x] == '^')
        {
            var left = CountSplit(y + 1, x - 1);
            var right = CountSplit(y + 1, x + 1);
            var countSplit = left + right;
            visited[(yOrig, x)] = countSplit;
            return countSplit;
        }
        y++;
    }
}