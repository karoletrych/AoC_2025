using System;
using System.Collections.Generic;
using System.Linq;
using static System.IO.File;

var inputPath = Helpers.Helpers.InputPath();
var input = (await ReadAllLinesAsync(inputPath));

var topNumbers = new List<int>();

foreach (var line in input)
{
    var array = new int[2];
    var used = new HashSet<int>();
    var usedIdx = 0;
    for (int i = line.Length - 1; i >= 0; i--)
    {
        if ((int)line[i] > array[0])
        {
            array[0] = (int)line[i];
            usedIdx = i;
        }
    }
    used.Add(usedIdx);
    
    for (int i = line.Length - 1; i >= 0; i--)
    {
        if ((int)line[i] > array[0])
        {
            array[1] = (int)line[i];
            usedIdx = i;
        }
    }
    used.Add(usedIdx);
    
    
    var topNumber = array[0] *10 + array[1];
    Console.WriteLine($"{topNumber} {string.Join(',', used)}");
    topNumbers.Add(topNumber);
}

Console.WriteLine(topNumbers.Sum());