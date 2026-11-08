using System.Diagnostics;
using Helpers;
using static System.IO.File;

var inputPath = Helpers.Helpers.InputPath();
var input = (await ReadAllLinesAsync(inputPath))
    .Select(z=>z.Select(y=>y).ToArray()).ToArray().ToMatrix();

var columns = new long[5, 1000];
var operators = new char[1000];

// for (var index = 0; index < 4; index++)
// {
//     var line = input[index];
//     var inline = line.Split().Where(z=>string.IsNullOrEmpty(z)==false).ToList();
//     for (int i = 0; i < 1000; i++)
//     {
//         columns[index,i] = int.Parse(inline[i]);
//     }
// }

long grandTotal = 0;
bool newNumber = false;
int offset = 0;
var numberIndex = 0;
List<long> buffer = new List<long>();
char op = ' ';
checked
{
    
for (var index = 3771; index >= 0; index--)
{
    var column = input.GetColumn(index);
    if (column.All(z => z == ' '))
    {
        if (op == '*')
        {
            grandTotal += buffer.Aggregate(1, (long a, long b) => a * b);
        }
        if (op == '+')
        {
            grandTotal += buffer.Sum();
        }
        buffer.Clear();
        continue;
    }
    
    int order = 0;
    long numberInThisColumn = 0;
    for (int i = column.Length - 1; i >= 0; i--)
    {
        if (column[i] == '*' || column[i] == '+')
        {
            op = column[i];
        }
        if (!char.IsDigit(column[i]))
            continue;
        var pow = (long)Math.Pow(10, order);
        order++;
        var d = column[i] - '0';
        numberInThisColumn += d * pow;


    }
    buffer.Add((int)numberInThisColumn);
  
   
}
}
if (op == '*')
{
    grandTotal += buffer.Aggregate(1, (long a, long b) => a * b);
}
if (op == '+')
{
    grandTotal += buffer.Sum();
}
buffer.Clear();

Console.WriteLine(grandTotal);

