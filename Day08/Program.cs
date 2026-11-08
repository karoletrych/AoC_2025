using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Helpers;
using static System.IO.File;

var input = Helpers.Helpers.LoadInput();


var vectors = input.Select(i => new IntVector(i.Split(",")[0], i.Split(",")[1], i.Split(",")[2]))
    .ToList();

var enumerable = vectors.SelectMany((z, i) => 
    vectors.Skip(i + 1), (x, y) => (vector: x, otherVector: y));


var circuits = new List<HashSet<IntVector>>();

var ordered = enumerable.OrderBy(z=> z.vector.Distance(z.otherVector)).ToList();

(IntVector vector, IntVector otherVector) pair = (new IntVector(), new IntVector());
var bigOne = new HashSet<IntVector>();
var it = ordered.GetEnumerator();
// for (int i = 0; i < 1000; i++)
while (bigOne.Count != vectors.Count)
{
    pair = it.Current;
    Console.WriteLine($"{pair.vector} : {pair.otherVector}");
    var alreadyInCircuit1 = circuits
        .FirstOrDefault(z=>z.Contains(pair.vector));
    var alreadyInCircuit2 = circuits
        .FirstOrDefault(z=>z.Contains(pair.otherVector));
    if (alreadyInCircuit1 != null && alreadyInCircuit2 != null && alreadyInCircuit1 == alreadyInCircuit2)
    {
    }
    else if (alreadyInCircuit1 != null && alreadyInCircuit2 == null)
    {
        alreadyInCircuit1.Add(pair.vector);
        alreadyInCircuit1.Add(pair.otherVector);
    }
    else if (alreadyInCircuit1 == null && alreadyInCircuit2 != null)
    {
        alreadyInCircuit2.Add(pair.vector);
        alreadyInCircuit2.Add(pair.otherVector);
    }
    else if (alreadyInCircuit1 != null && alreadyInCircuit2 != null)
    {
        foreach (var from1 in alreadyInCircuit1)
        {
            alreadyInCircuit2.Add(from1);
        }
        alreadyInCircuit1.Clear();
        
        alreadyInCircuit2.Add(pair.vector);
        alreadyInCircuit2.Add(pair.otherVector);
    }
    else
    {
        circuits.Add([pair.vector, pair.otherVector]);
    }

    bigOne = circuits.MaxBy(c => c.Count);
    it.MoveNext();
}
var circuitsSorted = circuits.OrderByDescending(z=>z.Count).ToList();

Console.WriteLine(pair.vector.X * pair.otherVector.X);

