namespace Helpers;

public static class Helpers
{
    private static readonly string ProjectDirectory = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;

    public static async Task<char[,]> Load2DArray() =>
        (await File.ReadAllLinesAsync(InputPath()))
        .Select(z=>z.Select(y=>y).ToArray()).ToArray().ToMatrix();

    [Obsolete]
    public static string InputPath() => Path.Combine(ProjectDirectory, "input", "input.txt");
    private static string TestInputPath() => Path.Combine(ProjectDirectory, "input", "testinput.txt");
    

    public static string[] LoadInput()
    {
        var inputPath = InputPath();
        return File.ReadAllLines(inputPath);
    }
    
    public static string[] LoadTestInput()
    {
        var inputPath = TestInputPath();
        return File.ReadAllLines(inputPath);
    }
}