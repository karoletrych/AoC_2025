using static System.IO.File;

var inputPath = Helpers.Helpers.InputPath();
var input = (await ReadAllLinesAsync(inputPath));

var topNumbers = new List<long>();

foreach (var line in input)
{
    var topNumber = FindTopNumber(line);

    topNumbers.Add(topNumber);
}

Console.WriteLine(topNumbers.Sum());

long FindTopNumber(string s)
{
    long topNumber = 0;
    var topDigits = new int[12];
    var digitIdx = 0;

    for (int i = 0; i < s.Length; i++)
    {
        if (s[i] - '0' > topDigits[0])
        {
            topDigits[digitIdx] = s[i] - '0';
            topDigits[digitIdx + 1] = 0;
            topNumber = TopNumberRec(i, digitIdx + 1);
        }
    }

    long TopNumberRec(int i, int digitIdx)
    {
        for (int j = i + 1; j < s.Length; j++)
        {
            if (s[j] - '0' > topDigits[digitIdx])
            {
                topDigits[digitIdx] = s[j] - '0';
                
                if (digitIdx == 11)
                {
                    var newTopNumber = ArrToNumber(topDigits);
                    if (newTopNumber > topNumber)
                    {
                        topNumber = newTopNumber;
                    }
                }
                else
                {
                    topDigits[digitIdx + 1] = 0;
                    TopNumberRec(j, digitIdx + 1);
                }
            }
        }

        return topNumber;
    }
    
    return topNumber;
}

static long ArrToNumber(int[] ints) => 
    ints.Select((t, i) => t * (long)Math.Pow(10, ints.Length - (i+1))).Sum();

