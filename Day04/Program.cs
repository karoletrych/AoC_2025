using System;
using System.Collections.Generic;
using System.Linq;
using static System.IO.File;

var inputPath = Helpers.Helpers.InputPath();
var input = await ReadAllLinesAsync(inputPath);

char [,] grid = new char[input.Length, input[0].Length];

for (int j = 0; j < input.Length; j++) 
    for (int i = 0; i < input.Length; i++)
        grid[i, j] = input[i][j];

var gridcopy = grid.Clone() as char[,];

var height = grid.GetLength(1);
var width = grid.GetLength(0);
var count = 0;

while (true)
{
    var temp = 0;
    for(int i= 0; i< width; i++)
    {
        for(int j = 0; j < height; j++)
        {
            var sum = Neigh(i, j, width, height, grid);
            if (grid[i,j] == '@' && sum < 4)
            {
                count++;
                temp++;
                grid[i,j] = 'x';
            }
        }
    }

    for (int i = 0; i < width; i++)
        for(int j = 0; j < height; j++)
            if (grid[i,j] == 'x') grid[i,j] = '.';
    
    if (temp == 0) break;
}


Console.WriteLine(count);

int Neigh(int i1, int i2, int width1, int height1, char[,] chars)
{
    var sum1 = 0;
    sum1 += IsRollOfPaper(i1-1, i2-1);
    sum1 += IsRollOfPaper(i1-1, i2);
    sum1 += IsRollOfPaper(i1-1, i2+1);
    sum1 += IsRollOfPaper(i1, i2-1);
    sum1 += IsRollOfPaper(i1, i2+1);
    sum1 += IsRollOfPaper(i1+1, i2-1);
    sum1 += IsRollOfPaper(i1+1, i2);
    sum1 += IsRollOfPaper(i1+1, i2+1);
    return sum1;

    int IsRollOfPaper(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width1 && y < height1 && chars[x, y] == '@')
            return 1;
        else
            return 0;
    }
}

