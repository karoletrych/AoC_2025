namespace Helpers;

public static class Extensions
{
    public static T[,]ToMatrix<T>(this T[][] matrix)
    {
        var arr2d = new T[matrix[0].Length, matrix.Length];
        for(int i = 0; i < matrix.Length; i++)
        for (int j = 0; j < matrix[0].Length; j++)
        {
            arr2d[j, i] = matrix[i][j];
        }

        return arr2d;
    }
    
    public static bool HasIndex<T>(this T[,] matrix, int columnNumber, int rowNumber) => 
        columnNumber >= 0 && columnNumber < matrix.GetLength(0) && rowNumber >= 0 && rowNumber < matrix.GetLength(1);

    public static T[] GetColumn<T>(this T[,] matrix, int columnNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(1))
            .Select(y => matrix[columnNumber, y])
            .ToArray();
    }

    public static T[] GetRow<T>(this T[,] matrix, int rowNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(0))
            .Select(x => matrix[x, rowNumber])
            .ToArray();
    }

    public static (int x, int y) FindItem<T>(this T[,] matrix, T s)
    {
        for(int i = 0; i < matrix.GetLength(0); i++)
            for(int j = 0; j < matrix.GetLength(1); j++)
                if(matrix[i,j]?.Equals(s) == true)
                    return (i,j);
        return (-1,-1);
    }
    
    public static T[,] Transpose<T>(this T[,] matrix)
    {
        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);
        var result = new T[cols, rows];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[j, i] = matrix[i, j];
            }
        }

        return result;
    }

    public static T[,] To2DArray<T>(this T[][] source)
    {
        if (source == null)
        {
            throw new ArgumentNullException("source");
        }

        int max = source.Select(l => l).Max(l => l.Count());

        var result = new T[max, source.Length];

        for (int i = 0; i < source.Length; i++)
        {
            for (int j = 0; j < source[i].Count(); j++)
            {
                result[j, i] = source[i][j];
            }
        }

        return result;
    }
    
    public static string ToBinaryString(this int i)
    {
        return i.ToString("B").PadLeft(32, '0');
    }
}