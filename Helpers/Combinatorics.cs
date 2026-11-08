namespace Helpers;

public static class Combinatorics
{
    public static IEnumerable<IList<T>> GetPermutations<T>(this List<T> list, int start)
    {
        if (start == list.Count)
        {
            var result = new T[list.Count];
            list.CopyTo(result, 0);
            yield return result;
            yield break;
        }

        for (int i = start; i < list.Count; i++)
        {
            Swap(list, start, i);
            foreach (var perm in GetPermutations(list, start + 1))
                yield return perm;
            Swap(list, start, i);
        }
    }

    private static void Swap<T>(IList<T> list, int i, int j)
    {
        if (i == j) return;
        (list[i], list[j]) = (list[j], list[i]);
    }
}