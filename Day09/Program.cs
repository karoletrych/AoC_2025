using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Helpers;
using static System.IO.File;

var inputPath = Helpers.Helpers.InputPath();
var input = (await ReadAllLinesAsync(inputPath));

var coords = input.Select(z=> (long.Parse(z.Split(",")[0]), long.Parse(z.Split(",")[1]))).ToList();

var width = coords.Max(z=>z.Item1) + 1;
var height = coords.Max(z=>z.Item2) + 1;

var map = new char[height][];
for (int j = 0; j < height; j++)
{
    map[j] = new char[width];
    for (int i = 0; i < width; i++) map[j][i] = '.';
}

foreach (var valueTuple in coords)
{
    map[valueTuple.Item2][valueTuple.Item1] = '#';
}

foreach (var valueTuple in coords.Zip(coords.Skip(1).Concat([coords.First()]), (a, b) => (a, b)))
{
    if (valueTuple.a.Item2 == valueTuple.b.Item2)
    {
        var start = Math.Min(valueTuple.a.Item1, valueTuple.b.Item1);
        var end = Math.Max(valueTuple.a.Item1, valueTuple.b.Item1);
        for (long i = start + 1; i < end; i++)
        {
            map[valueTuple.b.Item2][i] = '#';
        }    
    }

    if (valueTuple.a.Item1 == valueTuple.b.Item1)
    {
        var start = Math.Min(valueTuple.a.Item2, valueTuple.b.Item2);
        var end = Math.Max(valueTuple.a.Item2, valueTuple.b.Item2);
        for (long i = start + 1; i < end; i++)
        {
            map[i][valueTuple.b.Item1] = '#';
        }
    }
}

var isInPolygon = new bool[height][];
for (int j = 0; j < height; j++)
{
    isInPolygon[j] = new bool[width];
    bool inside = false;
    for (int i = 0; i < width; i++)
    {
        if (map[j][i] == '#')
        {
            isInPolygon[j][i] = true;

            int startI = i;
            while (i < width && map[j][i] == '#')
            {
                isInPolygon[j][i] = true;
                i++;
            }
            int endI = i - 1;

            for (int k = startI; k <= endI; k++)
            {
                if (j > 0 && map[j - 1][k] == '#')
                {
                    inside = !inside;
                }
            }
            
            i--; // adjust back because the outer loop will increment i
        }
        else
        {
            isInPolygon[j][i] = inside;
        }
    }
}

var descending = (from p1 in coords
    from p2 in coords
    where p1 != p2
    select (p1, p2, Area(p1, p2))).OrderByDescending(z=>z.Item3).ToList();
Console.WriteLine("start");
var solutions = new List<((long, long) p1, (long, long) p2, long area)>();
long maxArea = 0;
var it = 0;
foreach (var valueTuple in descending)
{
    if (valueTuple.Item3 <= maxArea) continue;
    
    var minX = Math.Min(valueTuple.p1.Item1, valueTuple.p2.Item1);
    var maxX = Math.Max(valueTuple.p1.Item1, valueTuple.p2.Item1);
    var minY = Math.Min(valueTuple.p1.Item2, valueTuple.p2.Item2);
    var maxY = Math.Max(valueTuple.p1.Item2, valueTuple.p2.Item2);

    if (!isInPolygon[valueTuple.p1.Item2][valueTuple.p1.Item1]) continue;
    if (!isInPolygon[valueTuple.p2.Item2][valueTuple.p1.Item1]) continue;
    if (!isInPolygon[valueTuple.p1.Item2][valueTuple.p2.Item1]) continue;
    if (!isInPolygon[valueTuple.p2.Item2][valueTuple.p2.Item1]) continue;

    bool tobreak = false;

    for (long i = minX + 1; i < maxX; i++)
    {
        if (!isInPolygon[minY][i])
        {
            tobreak = true;
            break;
        }
        if (!isInPolygon[maxY][i])
        {
            tobreak = true;
            break;
        }
    }

    if (tobreak)
        continue;

    for (long j = minY + 1; j < maxY; j++)
    {
        if (!isInPolygon[j][minX])
        {
            tobreak = true;
            break;
        }
        if (!isInPolygon[j][maxX])
        {
            tobreak = true;
            break;
        }
    }

    if (tobreak)
        continue;

    Console.WriteLine("Found solution: " + valueTuple);
    solutions.Add(valueTuple);
    maxArea = Math.Max(maxArea, valueTuple.Item3);
}

foreach (var solution in solutions.OrderByDescending(z=>z.area))
{
    Console.WriteLine("Final solution: " + solution);
}

long Area((long, long) p1, (long, long) p2)
{
    var width = Math.Abs(p1.Item1 - p2.Item1) + 1;
    var height = Math.Abs(p1.Item2 - p2.Item2) + 1;
    return width * height;
}
