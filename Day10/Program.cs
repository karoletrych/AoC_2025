using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Helpers;
using Microsoft.Z3;
using static System.IO.File;



var inputPath = Helpers.Helpers.InputPath();
var input = (await ReadAllLinesAsync(inputPath));

long totalPermutations = 0;

var lines = input.Select(Parse)
.ToList();

Console.WriteLine("parsed");

foreach (var line in lines)
{
    using var ctx = new Context();
    var opt = ctx.MkOptimize();

    // Create p0..pN integer variables for button presses
    var ps = line.ButtonsWiring.Select((_, idx) =>
    {
        var p = ctx.MkIntConst($"p{idx}");
        opt.Add(ctx.MkGe(p, ctx.MkInt(0)));  // p >= 0
        return p;
    }).ToList();

    // For each counter, sum all buttons affecting it and match target
    for (int xi = 0; xi < line.JoltageRequirements.Count; xi++)
    {
        var contrib = new List<ArithExpr>();
        for (int pi = 0; pi < line.ButtonsWiring.Count; pi++)
        {
            if (line.ButtonsWiring[pi].Contains(xi))
                contrib.Add(ps[pi]); // just add button press
        }
        opt.Add(ctx.MkEq(ctx.MkAdd(contrib), ctx.MkInt(line.JoltageRequirements[xi])));
    }

    // Minimize total button presses
    var X = ctx.MkAdd(ps);
    opt.MkMinimize(X);

    // Solve
    if (opt.Check() == Status.SATISFIABLE)
    {
        var m = opt.Model;
        Console.WriteLine("Solution for line:");
        Console.WriteLine($"Total button presses = {m.Evaluate(X)}");
        for (int i = 0; i < ps.Count; i++)
        {
            var expr = m.Evaluate(ps[i]);
            Console.WriteLine($"p{i} = {expr}");
            int intValue = ((IntNum)expr).Int;
            totalPermutations += intValue;

        }
        Console.WriteLine();
    }
    else
    {
        Console.WriteLine("No solution found for this line.");
    }
}


Console.WriteLine(totalPermutations);


Line Parse(string s1)
{
    var buttonsWiring = s1.Split("]")[1].Split("{")[0];
    var buttones = buttonsWiring.Split(") (");
    var d = buttones.Select(z => z.Trim(" ").TrimEnd(')').TrimStart('(').ToString()).ToList();

    var buttonsArrays = d
        .Select(s => s.Split(",").Select(int.Parse).ToList()).ToList();


    var joltages = s1.Split("{")[1].Trim(" ").Trim("}").ToString();
    
    var joltageNumbers = joltages.Split(",")
        .Select(z => int.Parse(z.ToString()))
        .ToList();
    
    return new Line
    {
        ButtonsWiring = buttonsArrays,
        JoltageRequirements = joltageNumbers,
    };
}

int ToIntMask(List<int> ints)
{
    int mask = 0;
    foreach (var i in ints)
    {
        mask |= 1 << i;
    }

    return mask;
}


record Line
{
    public List<List<int>> ButtonsWiring { get; set; }
    public List<int> JoltageRequirements { get; set; }
}
